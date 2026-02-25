## Smart Charity Food Management API

This project is a collaborative **ASP.NET Core Web API** designed to streamline the management of food donations and distribution for charitable organizations. It was built using a **Layered Architecture** and **Entity Framework Core** to ensure scalability, security, and professional code standards.

---

### ### 🤝 Team Collaboration & GitHub Flow

To maintain a stable development environment, our team followed a strict **Sequential Integration Workflow**. This ensured that the foundation was solid before adding complex business logic.

* **Developer 1 (Foundation):** Set up the project structure, configured the SQL Server `DonorDbContext`, and implemented the core **Donors Management** CRUD operations.
* **Developer 2 (Donation Logic):** Integrated **Food Donations**, linking donations to specific Donors and implementing quantity validation.
* **Developer 3 (Distribution System):** Developed the **Distribution Requests** module, incorporating business rules to check available quantities and donation status before distribution.

**Collaboration Rules:**

* **GitHub Flow:** Each feature was developed on a separate branch and required a **Pull Request (PR)** and successful merge into `main` before the next developer could start.
* **Database Syncing:** After every merge, team members used `Update-Database` to stay in sync with schema changes.

---

### ### 🛠 Technical Features

* **Architecture:** Adherence to **Layered Architecture** principles, separating Entities from Data Transfer Objects (DTOs).
* **Security & Stability:** Implementation of **Request and Response DTOs** to prevent "Object Cycle" errors and protect internal database structures.
* **API Standards:** Utilization of **Proper HTTP Status Codes** (e.g., `201 Created`, `204 No Content`, `400 Bad Request`).
* **Documentation:** Interactive testing available via **Swagger UI** at `/swagger/index.html`.
* **Bonus Features:** Included **Server-side Pagination** for the donor listing to optimize performance.

---
