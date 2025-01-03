# Multi-Vendor E-Commerce Platform

The **Multi-Vendor E-Commerce Platform** is a robust application designed for managing an online marketplace. It facilitates seamless interactions between vendors and customers while adhering to high-performance standards and leveraging modern technologies.  

## Features  

- **Order Notifications**:  
  - Sends an email notification to customers upon placing an order.  
  - Sends a notification to vendors if an order is not shipped within the defined timeframe.  

- **Real-Time Updates**:  
  - Implements **SignalR** for real-time updates, ensuring instant communication between the server and connected clients.  

- **Message Queuing**:  
  - Uses **RabbitMQ** for efficient and reliable message handling, ensuring asynchronous communication between services.  

- **Database Management**:  
  - Built on **SQL Server** to maintain data integrity and support complex queries.  
  - Adheres to **ACID principles** to guarantee data consistency and reliability.  

- **Technology Stack**:  
  - Backend: **.NET Core**  
  - Database: **SQL Server**  
  - Messaging: **RabbitMQ**  
  - Real-Time Communication: **SignalR**  

## Installation  

1. **Clone the Repository**:  
   ```bash
   git clone https://github.com/mojtabafarzaneh/Multi-Vendor-E-Commerce-Platform.git
   ```
2. **Navigate to the Project Directory:
   ```bash
   cd Multi-Vendor-E-Commerce-Platform
   ```
3. Set Up the Database:
   - Create a database in **SQL Server**.
   - Run the provided **SQL migration** scripts to set up the necessary tables and data.
4. Configure the Application:
   - Update the appsettings.json file with your database connection string and RabbitMQ configuration.
5. Run the Application:
   - Use Visual Studio or the .NET CLI to build and run the application.
   - Open a browser and navigate to https://localhost:[5001] to access the platform.




