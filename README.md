# Gym Management System Documentation

## Table of Contents
1. [Overview](#overview)
2. [System Architecture](#system-architecture)
3. [Enums](#enums)
4. [Models](#models)
5. [Services](#services)
6. [Class Relationships](#class-relationships)
7. [Design Patterns](#design-patterns)
8. [Key Features](#key-features)
9. [Business Rules](#business-rules)
10. [Technical Implementation Details](#technical-implementation-details)

## Overview

The Gym Management System is a comprehensive C# application designed to manage gym operations, including member management, trainer sessions, dietician consultations, and membership plans. The system follows object-oriented design principles with a clear separation of concerns across different modules, implementing modern software architecture patterns for maintainability and scalability.

## System Architecture

The system follows a **layered architecture** with clear separation of concerns:

- **Domain Layer**: Core business entities and interfaces (Models)
- **Business Logic Layer**: Service classes implementing business rules and workflows (Services)
- **Data Layer**: Value objects and data management (Email, PhoneNumber)
- **User Interface Layer**: User interaction through services with MessageBox and InputBox

## Enums

The Enums directory contains enumeration types that provide type-safe constants for the application:

### CoachType.cs
Defines specialized coaching roles available in the gym:
- **CrossFit**: Specializes in CrossFit training methodologies
- **PowerLifter**: Focuses on strength and power lifting techniques
- **PersonalTrainer**: General personal training across multiple disciplines
- **Lifestyle**: Specializes in lifestyle and wellness coaching

### DietitianType.cs
Categorizes dietician specializations:
- **WeightLoss**: Focuses on weight management programs and nutritional counseling
- **SportDietitian**: Specializes in nutrition for athletes and performance enhancement
- **SportNutritionist**: Provides sports-specific nutritional guidance and meal planning
- **CommunityDietitian**: Works with community health programs and public nutrition

### PlanType.cs
Defines membership plan categories:
- **BasicAccess**: Standard membership with basic facilities access and limited services
- **PremiumAccess**: Premium membership with additional benefits, unlimited access, and included services

### SessionStatus.cs
Tracks the status of training sessions and consultations:
- **Booked**: Session has been scheduled but not yet completed
- **Completed**: Session has been successfully conducted
- **Cancelled**: Session was cancelled prior to the scheduled time
- **NoShow**: Client did not attend the scheduled session

## Models

The Models directory contains the core business logic classes organized by functional areas:

### Member-Related Classes

#### GymMember.cs (Abstract Base Class)
Abstract base class for all member types with common properties:
- **Core Properties**: Name, Email, Phone Number, Membership Plan
- **Behavioral Properties**: Auto-renewal options, joining date
- **Abstract Methods**: `CalculateDiscount()`, `GetMembershipBenefits()`
- **Calculated Properties**: Age calculation using `GetAge()` method with DateTime validation

#### RegularGymMembers.cs
Implementation for basic membership tier:
- **Discount Rate**: 5% flat discount on services
- **Access Level**: Limited access to gym equipment during regular hours
- **Additional Services**: Limited access to group classes, upgrade eligibility
- **Session Allowance**: 2 personal trainer sessions per month
- **Key Methods**: `CanUpgradeToPremium()`, `GetMonthlyFee()`

#### PremiumGymMembers.cs
Implementation for premium membership tier:
- **Discount Rate**: 15% discount on additional services
- **Access Level**: Unlimited gym access, premium facilities
- **Included Services**: Free group classes, monthly dietician consultation
- **Session Allowance**: 4 personal trainer sessions per month
- **Advanced Features**: Session booking, cancellation with 24-hour policy, upcoming sessions tracking
- **Key Methods**: `BookPersonalTrainerSession()`, `GetRemainingSessions()`, `CancelSession()`

#### MembershipPlan.cs
Implements IMembershipPlan interface:
- **Plan Details**: Duration, fees, plan type, description
- **Session Allocations**: Personal trainer sessions, nutrition consultations, guest passes
- **Features**: Dynamic feature list for flexible plan configuration
- **Pricing**: Base pricing with discount application methods
- **Key Methods**: `CalculateMembershipPrice()`, `GetMembershipBenefits()`

### Staff-Related Classes

#### GymCoach.cs
Represents trainers with various specializations:
- **Professional Details**: Name, specialization (CoachType), certifications
- **Experience Tracking**: Dynamic experience calculation using `GetYearsOfExperience()`
- **Session Management**: Workout plan creation, member coaching
- **Hire Date**: DateTime-based experience calculation instead of static integer
- **Key Methods**: `GetYearsOfExperience()`, `CreateWorkoutPlan()`, `GetCertifications()`

#### Dietician.cs
Represents nutrition specialists:
- **Specializations**: Different focus areas defined by DietitianType
- **Services**: Meal plan creation, consultation scheduling
- **Professional Credentials**: Certifications and consultation rates
- **Key Methods**: `CreateMealPlan()`, `ScheduleConsultation()`, `CalculateCalories()`

#### IStaffMembers.cs
Interface defining common properties for staff:
- **Common Properties**: Name, salary, schedule, performance duties
- **Standardized Methods**: `GetSchedule()`, `PerformDuties()`

### Session Management

#### TrainerSession.cs
Represents a booked session between a member and coach:
- **Session Details**: Member, coach, date/time, status
- **Tracking**: Session ID, booking date, completion status
- **Status Management**: Booked, completed, cancelled, no-show tracking

#### TrainerSessionManager.cs
Manages booking, cancellation, and tracking of training sessions:
- **Booking Operations**: Session scheduling with validation
- **Availability Management**: Coach and time slot availability
- **Session Tracking**: History and upcoming sessions
- **Business Rules**: Minimum booking time, cancellation policies

#### DieticianConsultation.cs
Represents consultation sessions with dieticians:
- **Consultation Details**: Member, dietician, consultation type, notes
- **Meal Planning**: Integration with meal plan creation
- **Follow-up Tracking**: Progress monitoring and follow-up scheduling

#### DieticianSessionManager.cs
Manages booking and tracking of dietician consultations:
- **Consultation Booking**: Specialized booking for nutrition sessions
- **Meal Plan Integration**: Coordination with meal planning services
- **Progress Tracking**: Member progress and consultation history

#### ISessionManager.cs
Interface defining common session management operations:
- **Standard Operations**: Book, cancel, reschedule sessions
- **Availability Checking**: Time slot and staff availability
- **Session Retrieval**: Getting session details and history

### Supporting Classes

#### Gym.cs
Central class that coordinates members, staff, and operations:
- **Member Management**: Adding, removing, and managing members
- **Staff Coordination**: Coach and dietician management
- **Facility Operations**: Equipment management, membership lists
- **Business Operations**: Revenue tracking, membership statistics

#### IGym.cs
Interface defining gym operations contract:
- **Core Operations**: Member and staff management contracts
- **Service Coordination**: Session booking and management interfaces
- **Reporting**: Membership and revenue reporting requirements

#### Email.cs
Value object representing email addresses with validation:
- **Validation**: Email format validation and domain checking
- **Immutability**: Read-only email representation
- **Utility Methods**: Email domain extraction and validation

#### PhoneNumber.cs
Value object for phone numbers with country/area code support:
- **International Support**: Country code and area code management
- **Validation**: Phone number format validation
- **Formatting**: Consistent phone number display formatting

## Services

The Services directory implements the application's business processes and user interactions:

### MemberManagementService.cs
Handles all member-related operations:
- **Member Registration**: Adding new regular and premium members with proper validation
- **Member Removal**: Safe member removal with data integrity checks
- **Payment Processing**: Processing payments for additional fees and services
- **Membership Upgrades**: Managing premium upgrade eligibility and conversions
- **Auto-renewal Management**: Setting up and managing subscription renewals
- **Session Allocation**: Ensuring proper membership plan assignment with correct session allowances

### SessionManagementService.cs
Manages booking operations:
- **Trainer Session Booking**: Scheduling trainer sessions with availability checking
- **Dietician Consultation Booking**: Specialized booking for nutrition consultations
- **Input Validation**: Comprehensive validation for dates, times, and booking constraints
- **Session Management**: Lookup, cancellation, and rescheduling operations
- **Availability Display**: Showing session details and staff availability
- **User Feedback**: Providing detailed feedback for booking success/failure scenarios

### StaffManagementService.cs
Handles operations related to gym staff:
- **Staff Recruitment**: Adding new coaches with different specializations
- **Dietician Management**: Adding dieticians with various expertise areas
- **Assignment Management**: Managing staff assignments and availability schedules
- **Staff Transitions**: Handling staff departures and replacement procedures
- **Performance Tracking**: Monitoring staff performance and member satisfaction

## Class Relationships

The system implements several key relationships:

### Inheritance Hierarchy
- **GymMembers** (Abstract) → **RegularGymMembers**, **PremiumGymMembers**
- **IStaffMembers** → **GymCoach**, **Dietician**
- **ISessionManager** → **TrainerSessionManager**, **DieticianSessionManager**

### Composition Relationships
- **Gym** contains **GymMembers**, **GymCoach**, **Dietician**
- **GymMembers** has **MembershipPlan**, **Email**, **PhoneNumber**
- **TrainerSession** contains **GymMember**, **GymCoach**

### Aggregation Relationships
- **MembershipPlan** aggregates **Features** (List<string>)
- **Gym** aggregates **MembershipPlan** options

## Design Patterns

The system implements several design patterns:

### Template Method Pattern
- **GymMember** abstract class defines operation skeletons
- **Subclasses** override specific behaviors like `CalculateDiscount()` and `GetMembershipBenefits()`

### Strategy Pattern
- **Different discount calculation algorithms** implemented by different member types
- **Flexible membership benefit calculation** based on membership tier

### Factory Method Pattern
- **MemberManagementService** acts as a creator for different member types
- **Encapsulates member creation logic** with proper initialization

### Command Pattern
- **Session booking** encapsulates operations as objects with execution history
- **Undo/redo capabilities** for session management operations

### Observer Pattern
- **Auto-renewal subscription system** tracks members for renewal notifications
- **Event-driven architecture** for membership status changes

### Singleton-like Pattern
- **Central Gym class instance** shared across services
- **Consistent state management** across the application

### Composite Pattern
- **Individual members and collections** treated uniformly through consistent interfaces
- **Hierarchical structure** for membership management

## Key Features

### Dynamic Calculations
- **Age Calculation**: Real-time age calculation using birth date and current date
- **Experience Calculation**: Dynamic coach experience calculation based on hire date
- **Session Tracking**: Monthly session allocation and usage tracking

### Flexible Membership System
- **Tiered Membership**: Regular and Premium tiers with distinct benefits
- **Upgrade Path**: Seamless upgrade from Regular to Premium membership
- **Auto-renewal**: Automated subscription renewal system

### Comprehensive Session Management
- **Multi-type Sessions**: Personal training and dietician consultations
- **Booking Policies**: Advance booking requirements and cancellation policies
- **Session Tracking**: Complete session history and upcoming session management

### Staff Management
- **Specialized Roles**: Different coach types and dietician specializations
- **Dynamic Experience**: Real-time experience calculation for staff
- **Performance Tracking**: Staff performance and member satisfaction monitoring

## Business Rules

### Membership Rules
- **Regular Members**: 2 personal trainer sessions per month, 5% discount
- **Premium Members**: 4 personal trainer sessions per month, 15% discount
- **Upgrade Eligibility**: Regular members can upgrade to premium at any time
- **Auto-renewal**: Configurable automatic subscription renewal

### Session Booking Rules
- **Advance Booking**: Sessions must be booked at least 2 hours in advance
- **Cancellation Policy**: 24-hour advance notice required for cancellations
- **Monthly Allocation**: Session limits reset monthly based on membership tier
- **No-show Policy**: No-show sessions count against monthly allocation

### Staff Rules
- **Experience Calculation**: Based on hire date, not static values
- **Specialization Requirements**: Coaches must have defined specializations
- **Certification Tracking**: Staff certifications and credentials management

## Technical Implementation Details

### Data Types
- **Decimal for Financial**: All monetary calculations use decimal type for precision
- **DateTime for Dates**: Comprehensive date/time handling with validation
- **Enums for Type Safety**: Strong typing for categories and status values

### Error Handling
- **Comprehensive Validation**: Input validation at multiple layers
- **Specific Exceptions**: Custom exception types for different error scenarios
- **User-friendly Messages**: Clear error messages for user interactions

### Performance Considerations
- **Session Caching**: Efficient session lookup and management
- **Lazy Loading**: Deferred loading of member session history
- **Optimized Queries**: Efficient data retrieval for large member bases

### Security Features
- **Data Validation**: Comprehensive input validation and sanitization
- **Access Control**: Role-based access to different system functions
- **Data Integrity**: Referential integrity checks and constraints

### Extensibility
- **Interface-based Design**: Easy addition of new member types and services
- **Plugin Architecture**: Extensible staff types and session types
- **Configuration-driven**: Feature toggles and configurable business rules
