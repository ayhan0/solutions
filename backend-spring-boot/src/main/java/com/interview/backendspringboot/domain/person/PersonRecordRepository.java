package com.interview.backendspringboot.domain.person;


import org.springframework.data.domain.*;
import org.springframework.data.jpa.repository.*;
import org.springframework.data.repository.query.Param;
import org.springframework.transaction.annotation.Transactional;

public interface PersonRecordRepository extends JpaRepository<PersonRecord, Long> {

    @Query("""
        select count(p)
        from PersonRecord p
        where p.active = true and lower(p.name) = lower(:name)
        """)
    long countActiveByName(@Param("name") String name);

    @Query("""
        select p.id as id, p.name as name, p.surname as surname, p.age as age,
               p.email as email, p.phone as phone, p.active as active,
               p.createdAt as createdAt, p.updatedAt as updatedAt
        from PersonRecord p
        where p.active = true
        order by p.id desc
        """)
    Page<PersonView> listActive(Pageable pageable);

    @Query("""
        select p.id as id, p.name as name, p.surname as surname, p.age as age,
               p.email as email, p.phone as phone, p.active as active,
               p.createdAt as createdAt, p.updatedAt as updatedAt
        from PersonRecord p
        where p.active = true
          and (:name is null or lower(p.name) like lower(concat('%', :name, '%')))
          and (:phone is null or p.phone like concat('%', :phone, '%'))
        order by p.id desc
        """)
    Page<PersonView> searchActive(@Param("name") String name,
                                  @Param("phone") String phone,
                                  Pageable pageable);

    @Modifying
    @Transactional
    @Query("""
        update PersonRecord p
        set p.active = false
        where p.active = true and p.phone = :phone
        """)
    int softDeleteByPhone(@Param("phone") String phone);

    @Modifying
    @Transactional
    @Query("""
        update PersonRecord p
        set p.active = false
        where p.active = true and lower(p.name) = lower(:name)
        """)
    int softDeleteByName(@Param("name") String name);
}