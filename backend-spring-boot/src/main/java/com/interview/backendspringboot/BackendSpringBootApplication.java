package com.interview.backendspringboot;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.data.jpa.repository.config.EnableJpaAuditing;

@SpringBootApplication
@EnableJpaAuditing
public class BackendSpringBootApplication {

    public static void main(String[] args) {
        SpringApplication.run(BackendSpringBootApplication.class, args);
    }

}
