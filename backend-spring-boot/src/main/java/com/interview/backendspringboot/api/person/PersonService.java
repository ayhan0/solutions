package com.interview.backendspringboot.api.person;


import com.interview.backendspringboot.config.ApiExceptions;
import com.interview.backendspringboot.domain.base.PageResponse;
import com.interview.backendspringboot.domain.person.*;
import lombok.RequiredArgsConstructor;
import org.springframework.cache.annotation.CacheEvict;
import org.springframework.cache.annotation.Cacheable;
import org.springframework.data.domain.*;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

@Service
@RequiredArgsConstructor
public class PersonService {

    private final PersonRecordRepository repo;
    private final PersonMapper mapper;

    @Transactional(readOnly = true)
    @Cacheable(
            cacheNames = "person:list",
            key = "'p:' + #page + ':s:' + #size"
    )
    public PageResponse<PersonResponse> list(int page, int size) {
        Pageable pageable = PageRequest.of(page, size, Sort.by(Sort.Direction.DESC, "id"));
        Page<PersonView> p = repo.listActive(pageable);

        List<PersonResponse> items = p.getContent().stream()
                .map(mapper::toResponse)
                .toList();

        return new PageResponse<>(items, p.getNumber(), p.getSize(), p.getTotalElements(), p.getTotalPages());
    }

    @Transactional(readOnly = true)
    @Cacheable(
            cacheNames = "person:search",
            key = "'n:' + (#name==null?'':#name.toLowerCase()) + ':ph:' + (#phone==null?'':#phone) + ':p:' + #page + ':s:' + #size"
    )
    public PageResponse<PersonResponse> search(String name, String phone, int page, int size) {
        Pageable pageable = PageRequest.of(page, size, Sort.by(Sort.Direction.DESC, "id"));
        Page<PersonView> p = repo.searchActive(name, phone, pageable);

        List<PersonResponse> items = p.getContent().stream()
                .map(mapper::toResponse)
                .toList();

        return new PageResponse<>(items, p.getNumber(), p.getSize(), p.getTotalElements(), p.getTotalPages());
    }

    @Transactional
    @CacheEvict(cacheNames = {"person:list", "person:search"}, allEntries = true)
    public PersonResponse add(PersonCreateRequest req) {
        PersonRecord entity = PersonRecord.builder()
                .name(req.name())
                .surname(req.surname())
                .age(req.age())
                .email(req.email())
                .phone(req.phone())
                .build();

        PersonRecord saved = repo.save(entity);


        return new PersonResponse(
                saved.getId(),
                saved.getName(),
                saved.getSurname(),
                saved.getAge(),
                saved.getEmail(),
                saved.getPhone(),
                saved.getActive(),
                saved.getCreatedAt(),
                saved.getUpdatedAt()
        );
    }

    @Transactional
    @CacheEvict(cacheNames = {"person:list", "person:search"}, allEntries = true)
    public void deleteByKey(String key) {
        boolean looksLikePhone = key != null && key.matches(".*\\d.*");

        int updated = looksLikePhone
                ? repo.softDeleteByPhone(key)
                : repo.softDeleteByName(key);

        if (updated == 0) {
            throw new ApiExceptions.NotFound("No active record found for key: " + key);
        }
    }
}