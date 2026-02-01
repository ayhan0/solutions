package com.interview.backendspringboot.domain.person;

import jakarta.validation.constraints.*;

public record PersonCreateRequest(
        @NotBlank @Size(max = 100) String name,
        @NotBlank @Size(max = 100) String surname,
        @NotNull @Min(0) @Max(150) Integer age,
        @Email @Size(max = 200) String email,
        @NotBlank @Size(max = 30) String phone
) {}