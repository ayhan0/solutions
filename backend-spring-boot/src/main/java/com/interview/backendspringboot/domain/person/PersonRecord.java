package com.interview.backendspringboot.domain.person;

import com.interview.backendspringboot.domain.base.BaseEntity;
import jakarta.persistence.*;
import lombok.*;

@Entity
@Table(
        name = "person_record",
        indexes = {
                @Index(name = "ix_person_record_name", columnList = "name"),
                @Index(name = "ix_person_record_phone", columnList = "phone", unique = true)
        }
)
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class PersonRecord extends BaseEntity {

    @Column(nullable = false, length = 100)
    private String name;

    @Column(nullable = false, length = 100)
    private String surname;

    @Column(nullable = false)
    private Integer age;

    @Column(length = 200)
    private String email;

    @Column(nullable = false, length = 30, unique = true)
    private String phone;
}