# Gym Management System

A simple Windows Forms application built in C# for managing gym members, staff, and membership plans. This project demonstrates object-oriented programming concepts including inheritance, polymorphism, abstraction, interfaces, SOLID principles and the GOF.

## 📋 Table of Contents
- [Features](#features)
- [Project Structure](#project-structure)
- [Classes Overview](#classes-overview)
- [OOP Concepts Demonstrated](#oop-concepts-demonstrated)
- [Requirements](#requirements)

## ✨ Features

- **Member Management**: Handle both Regular and Premium members
- **Staff Management**: Manage Coaches and Dieticians
- **Membership Plans**: Different plan types with pricing
- **Contact Information**: Email and phone number validation
- **Discount Calculations**: Automatic discount application based on member type
- **Staff Scheduling**: View staff schedules and duties

## 📁 Project Structure

```
GymManagementSystem/
├── Enums/
│   ├── CoachType.cs
│   ├── DieticianType.cs
│   └── PlanType.cs
├── Models/
│   ├── Coach.cs
│   ├── Dietician.cs
│   ├── Email.cs
│   ├── Gym.cs
│   ├── IGym.cs
│   ├── IMembershipPlan.cs
│   ├── IStaffMembers.cs
│   ├── Members.cs
│   ├── MembershipPlan.cs
│   ├── PhoneNumber.cs
│   ├── PremiumMembers.cs
│   └── RegularMembers.cs
├── Form1.cs
└── Program.cs
```

## 🏗️ Classes Overview

### Abstract Classes
- **Members**: Base class for all gym members with abstract methods for discount calculation and benefits

### Concrete Classes
- **RegularMembers**: Basic gym members with standard access
- **PremiumMembers**: Enhanced members with personal trainer sessions and nutrition consultations
- **Coach**: Staff members specializing in different training types
- **Dietician**: Nutrition specialists providing meal plans and consultations
- **Gym**: Main class managing all members, staff, and plans
- **MembershipPlan**: Defines membership types and pricing

### Interfaces
- **IMembershipPlan**: Contract for membership plan implementations
- **IStaffMembers**: Contract for all staff member types
- **IGym**: Contract for gym operations

### Value Objects
- **Email**: Email validation and management
- **PhoneNumber**: Phone number validation with country codes

### Enums
- **PlanType**: BasicAccess, PremiumAccess
- **CoachType**: CrossFit, PowerLifter, PersonalTrainer, Lifestyle
- **DieticianType**: WeightLoss

## 🎯 OOP Concepts Demonstrated

### 1. **Inheritance**
- `RegularMembers` and `PremiumMembers` inherit from abstract `Members` class
- `Coach` and `Dietician` implement `IStaffMembers` interface

### 2. **Polymorphism**
- Abstract methods `CalculateDiscount()` and `GetMembershipBenefits()` implemented differently in derived classes
- Interface methods implemented by multiple classes

### 3. **Abstraction**
- Abstract `Members` class defines common structure
- Interfaces define contracts without implementation

### 4. **Encapsulation**
- Properties with getters and setters
- Validation logic encapsulated in value objects
- Business logic contained within appropriate classes

### 5. **Composition**
- `Members` class contains `Email` and `PhoneNumber` objects
- `Gym` class contains collections of members and staff

## 📋 Requirements

- **Development Environment**: Visual Studio 2019+
- **Framework**: .NET Framework 4.7.2+
- **Platform**: Windows
- **Language**: C# 8.0+

## 🎓 Academic Context

This project is designed for Computer Science coursework to demonstrate:
- Object-oriented programming principles
- Class design and relationships
- Interface implementation
- Abstract class usage
- Enum definitions
- Basic validation techniques
- Windows Forms application structure

## 📝 Notes

- This is a simplified implementation for academic purposes
- Error handling is basic and can be enhanced
- No database integration - uses in-memory collections
- UI implementation is minimal, focusing on the class structure
- Validation is simplified for demonstration purposes

## 🤝 Contributing

This is an academic project. Feel free to fork and enhance for learning purposes.

## 📄 License

This project is for educational use only.
