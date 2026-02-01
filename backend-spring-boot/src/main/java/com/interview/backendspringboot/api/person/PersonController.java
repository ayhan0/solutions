package com.interview.backendspringboot.api.person;

import com.interview.backendspringboot.domain.base.PageResponse;
import com.interview.backendspringboot.domain.person.PersonCreateRequest;
import com.interview.backendspringboot.domain.person.PersonResponse;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.Page;
import org.springframework.web.bind.annotation.*;


@RestController
@RequiredArgsConstructor
@RequestMapping("/api/entity")
public class PersonController {

    private final PersonService service;

    @PostMapping("/add")
    public PersonResponse add(@RequestBody PersonCreateRequest req) {
        return service.add(req);
    }

    @DeleteMapping("/delete/{key}")
    public void delete(@PathVariable String key) {
        service.deleteByKey(key);
    }

    @GetMapping("/list")
    public PageResponse<PersonResponse> list(
            @RequestParam(defaultValue = "0") int page,
            @RequestParam(defaultValue = "20") int size
    ) {
        return service.list(page, size);
    }

    @GetMapping("/search/{key}")
    public PageResponse<PersonResponse> search(
            @PathVariable String key,
            @RequestParam(defaultValue = "0") int page,
            @RequestParam(defaultValue = "20") int size
    ) {
        boolean looksLikePhone = key != null && key.matches(".*\\d.*");
        String name = looksLikePhone ? null : key;
        String phone = looksLikePhone ? key : null;

        return service.search(name, phone, page, size);
    }
}