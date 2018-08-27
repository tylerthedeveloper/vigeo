# vigeo
A cross-platform mobile application that provides users with a personally curated list of local events and communities in their area, allowing them to connect and share new experiences with like minded people.

# Overview
Cross-platform mobile application built in Xamarin-Forms (c#).

• RESTful API (Python, flask)

• Abstraction in Xamarin “PCL” to delegate platform-specific functionality at runtime using dependency injection.


## Design Pattern

MVVM (model, view, view-model)

## Integrations

• AllEvents aggregator

• Facebook Graph API

• Realm for an on-phone cached database and offline storage

• Grial Ki for XAML UI templating.

• Postgres 

• Azure App Services

• Entity Framework (using Code-First (models) approach and Fluent API for DBO/DTO mapping (key/table relations))

• HockeyApp for continuous distribution to testers, crash reporting and analysis, and gathering user feedback.
