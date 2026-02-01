package com.interview.backendspringboot.security;


import com.interview.backendspringboot.config.ApiExceptions;
import com.interview.backendspringboot.domain.base.ApiMessage;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.http.*;
import org.springframework.web.bind.MethodArgumentNotValidException;
import org.springframework.web.bind.annotation.*;

@RestControllerAdvice
public class GlobalExceptionHandler {

    private static final Logger log = LoggerFactory.getLogger(GlobalExceptionHandler.class);

    @ExceptionHandler(ApiExceptions.NotFound.class)
    public ResponseEntity<ApiMessage> notFound(ApiExceptions.NotFound e) {
        return ResponseEntity.status(HttpStatus.NOT_FOUND).body(new ApiMessage(e.getMessage()));
    }

    @ExceptionHandler(ApiExceptions.Conflict.class)
    public ResponseEntity<ApiMessage> conflict(ApiExceptions.Conflict e) {
        return ResponseEntity.status(HttpStatus.CONFLICT).body(new ApiMessage(e.getMessage()));
    }

    @ExceptionHandler(ApiExceptions.BadRequest.class)
    public ResponseEntity<ApiMessage> badRequest(ApiExceptions.BadRequest e) {
        return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(new ApiMessage(e.getMessage()));
    }

    @ExceptionHandler(MethodArgumentNotValidException.class)
    public ResponseEntity<ApiMessage> validation(MethodArgumentNotValidException e) {
        return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(new ApiMessage("Validation error"));
    }

    @ExceptionHandler(Exception.class)
    public ResponseEntity<ApiMessage> fallback(Exception e) {
        log.error("Unexpected error", e);
        return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(new ApiMessage("Unexpected error"));
    }
}