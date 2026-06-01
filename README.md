# Ration Distribution System with LCD & SMS Integration

##  Project Overview

The Ration Distribution System with LCD & SMS Integration is a smart solution developed to improve transparency, efficiency, and communication in the public distribution process.

The system enables ration shop administrators to manage consumers, maintain stock records, distribute ration items, and automatically notify consumers through SMS alerts. An LCD display module can also be used to show distribution status and important information in real time.

This project aims to reduce manual errors, improve record management, and ensure that beneficiaries receive timely updates regarding ration availability and collection.

---

##  Objectives

- Digitize the ration distribution process.
- Maintain accurate records of consumers and ration stock.
- Provide automated SMS notifications.
- Improve transparency and accountability.
- Reduce paperwork and manual effort.
- Display important information through an LCD module.

---

## Features

### Consumer Management
- Register new consumers.
- Store consumer details securely.
- Manage ration card information.

### Stock Management
- Add and update ration stock.
- Monitor available inventory.
- Track stock consumption.

### Ration Distribution
- Record ration distribution transactions.
- Maintain distribution history.
- Generate transaction records.

### SMS Notification System
Consumers receive SMS alerts for:
- Ration stock arrival.
- Successful ration collection.
- Quantity distributed.
- Remaining balance (if applicable).
- Monthly reminders for uncollected ration.

### LCD Display Integration
- Display ration availability.
- Show transaction status.
- Display important announcements.

### Administration
- Manage consumers.
- Manage stock.
- View reports and records.
- Monitor system activity.

---

##  System Architecture

```text
+------------------+
|      Admin       |
+------------------+
         |
         v
+------------------+
| Web Application  |
| (Frontend)       |
+------------------+
         |
         v
+------------------+
| Backend Server   |
| Node.js/Express  |
+------------------+
         |
         v
+------------------+
|    MongoDB       |
|    Database      |
+------------------+
         |
         +-------------------+
         |                   |
         v                   v
+----------------+   +----------------+
| SMS Service    |   | LCD Display    |
+----------------+   +----------------+
         |
         v
+----------------+
|  Consumers     |
+----------------+
```

---

##  Technologies Used

### Frontend
- React.js
- HTML5
- CSS3
- JavaScript

### Backend
- Node.js
- Express.js

### Database
- MongoDB

### Hardware Integration
- LCD Display Module
- GSM/SMS Module

### Tools
- VS Code
- Git
- GitHub
- Postman

---

##  Screenshots

### Login Page
<img width="1600" height="754" alt="image" src="https://github.com/user-attachments/assets/b5356a46-3830-41cf-90b5-47639f36506d" />


### Consumer Dashboard
<img width="1600" height="740" alt="image" src="https://github.com/user-attachments/assets/4de6a969-ec44-4e01-98a9-cf79984e520f" />

<img width="1600" height="744" alt="image" src="https://github.com/user-attachments/assets/01a20f36-4ed3-48e5-b59d-cbff6322e9ef" />


### Consumer Management
<img width="1600" height="742" alt="WhatsApp Image 2026-04-21 at 8 23 05 PM" src="https://github.com/user-attachments/assets/dba7eeba-5022-4d44-bbb5-417b854a5387" />


### Stock Management
<img width="1600" height="748" alt="image" src="https://github.com/user-attachments/assets/c3691b17-1d72-4300-9d30-9e872205e3ba" />


### Distribution Module
![Distribution Module](images/distribution.png)

### SMS Notification
<img width="150" height="300" alt="image" src="https://github.com/user-attachments/assets/04ffb994-e1d1-428a-861c-e46556b45f35" />


### LCD Display
![LCD Display](images/lcd-display.png)

---

##  Workflow

1. Admin logs into the system.
2. Consumer information is registered.
3. Stock information is updated.
4. Ration distribution is recorded.
5. SMS notification is automatically sent.
6. LCD display updates the current status.
7. Distribution history is stored in the database.

---

##  Advantages

- Reduces manual paperwork.
- Improves transparency.
- Faster record management.
- Better communication with consumers.
- Automated notification system.
- Accurate stock tracking.
- Easy monitoring and reporting.

---

##  Future Enhancements

- QR Code Verification
- RFID-Based Authentication
- Mobile Application
- Aadhaar Integration
- Online Consumer Portal
- Real-Time Analytics Dashboard
- AI-Based Demand Prediction
- Biometric Verification
- Cloud Deployment

---

##  Academic Relevance

This project demonstrates practical implementation of:

- Database Management Systems
- Web Development
- Client-Server Architecture
- SMS Gateway Integration
- IoT and Embedded Systems
- Software Engineering Principles

---

##  Author

### Lokesh Anil Rangari

B.Tech Computer Science & Engineering

Jhulelal Institute of Technology

Nagpur, Maharashtra, India

GitHub: https://github.com/lokeTech

---

##  License

This project is developed for educational and academic purposes.

Feel free to use, modify, and enhance the project with proper attribution.

---

##  Support

If you found this project useful, please consider giving it a star on GitHub.

Thank you for visiting this repository!
