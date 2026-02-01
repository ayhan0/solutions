package com.interview.backendspringboot.domain.person;


import org.springframework.stereotype.Component;

@Component
public class PersonMapper {
    /*
     * MapStruct is intentionally not used here.
     * This mapper handles a single, straightforward projection-to-DTO conversion,
     * so a manual mapping keeps the code simpler and avoids unnecessary dependencies.
     */
    public PersonResponse toResponse(PersonView v) {
        return new PersonResponse(
                v.getId(),
                v.getName(),
                v.getSurname(),
                v.getAge(),
                v.getEmail(),
                v.getPhone(),
                v.getActive(),
                v.getCreatedAt(),
                v.getUpdatedAt()
        );
    }
}