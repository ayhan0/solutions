package com.interview.backendspringboot.domain.person;

import java.time.Instant;

public record PersonResponse(
        Long id,
        String name,
        String surname,
        Integer age,
        String email,
        String phone,
        Boolean active,
        Instant createdAt,
        Instant updatedAt
) {}