package com.interview.backendspringboot.domain.person;

import java.time.Instant;

public interface PersonView {
    Long getId();
    String getName();
    String getSurname();
    Integer getAge();
    String getEmail();
    String getPhone();
    Boolean getActive();
    Instant getCreatedAt();
    Instant getUpdatedAt();
}