# Exercise 1: Implementing the Singleton Pattern

## Objective

To implement the Singleton Design Pattern in Java and ensure that only one instance of the Logger class exists throughout the application lifecycle.

## Scenario

A logging utility is required in an application. To maintain consistent logging and avoid creating multiple logger objects, the Logger class should follow the Singleton Design Pattern.

## Files

### Logger.java

Implements the Singleton pattern by:

* Maintaining a private static instance of the Logger class.
* Using a private constructor to prevent external object creation.
* Providing a public static method `getInstance()` to access the single instance.

### SingletonTest.java

Tests the Singleton implementation by:

* Retrieving the Logger instance multiple times.
* Logging messages using the Logger object.
* Verifying that both references point to the same object.

## Implementation Steps

1. Create a Java project named `SingletonPatternExample`.
2. Create the `Logger` class.
3. Make the constructor private.
4. Create a private static instance variable.
5. Implement the `getInstance()` method.
6. Create a test class to verify the implementation.
7. Execute the program and observe the output.

## Expected Output

Logger Created

LOG: Application Started

LOG: User Logged In

true

## Explanation

* The Logger object is created only once.
* Subsequent calls to `getInstance()` return the existing object.
* The output `true` confirms that both references point to the same instance.
* This demonstrates the Singleton Design Pattern successfully.

## Concepts Used

* Object-Oriented Programming (OOP)
* Singleton Design Pattern
* Static Members
* Private Constructors
* Encapsulation

## Result

The Singleton Pattern was successfully implemented. Only one instance of the Logger class was created and shared throughout the application.
