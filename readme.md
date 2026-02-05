# CSIFlex Mobile Installation Guide

## Prerequisites

Before starting the installation, ensure that the following software and tools are installed:

1. **Visual Studio** – Required for development purposes.
2. **VS Code** – For lightweight code editing.
3. **.NET Framework 4.8** – Ensure it’s installed on your machine.
4. **.NET Core 3.1** – Necessary to support .NET Core applications.
5. **eNET Server Simulator** – You need this simulator for testing purposes.
6. **MySQL Workbench** – To manage the MySQL database.
7. **CSIFlex Server** – Required for the application to run.

---

## Local Setup

### Step 1: Install the CSIFlex Server

1. **Run `CSI Flex Server Installer.exe`.**
   - The installer will automatically set up a MySQL instance on `localhost`.

### Step 2: Access the MySQL Database

1. Once the CSIFlex Server is installed, access the MySQL database with the following credentials:
   - **Host**: `localhost`
   - **Port**: `3306`
   - **User**: `root`
   - **Password**: `CSIF1337`

### Step 3: Create the Configuration File

1. **Create a folder** on your machine at `C:/CSIFLEX`.
2. **Within this folder**, create a file named `CSIFLEX.config.json` and include the following content:

    ```json
    {
        "Company": {
            "CompanyId": "3d849d9d-71e5-41eb-93ef-b1b8edcebac4",
            "CompanyName": "ABC Solutions",
            "Division": "",
            "Address": "6020 Jean-Talon East, Suite 750",
            "City": "Montreal",
            "Province": "ON",
            "Country": "Canada",
            "PostalCode": "XYZ",
            "Contact": "Drausio",
            "Phone": "6134009460",
            "Email": "drausio@csiflex.com"
        },
        "CSIFLEXFolder": "I:/Projects/CSIFLEX 2.0/CSIFLEX.Management/bin",
        "IPAddress": "192.168.2.10",
        "PortBase": 6050,
        "AdminUser": "Admin",
        "AdminPwd": "FR9w5MzBxF/4f/bv3I2uTIBm8xgsj6StgFPi5OdlGU4=",
        "Modules": {
            "CSIFLEX_WebApp": {
                "Module": 13,
                "ModuleType": 0,
                "ModuleName": "CSIFLEX_WebApp",
                "ModuleVersion": "1.0.0.0",
                "ModuleDescription": "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed convallis lectus quis lorem finibus, vitae rhoncus metus vehicula. Duis erat ipsum, tempus et dolor id, posuere hendrerit risus.",
                "ModuleFolder": "C:/CSIFLEX/eNETDNC.WebApp",
                "ServiceName": "CSIFLEX.WebApp.Service",
                "IpAddress": "192.168.2.10",
                "Port": 8031,
                "ApiAddress": null,
                "Database": {
                    "Provider": "MYSQL",
                    "Server": "localhost",
                    "Port": 3306,
                    "DatabaseName": "csi_auth",
                    "User": "root",
                    "Password": "p0TpOhHDJorex7kIx5VJRQ=="
                },
                "Properties": {
                    "3d849d9d-71e5-41eb-93ef-b1b8edcebbd4": {
                        "PropertyId": "3d849d9d-71e5-41eb-93ef-b1b8edcebbd4",
                        "PropertyName": "eNetFolder",
                        "PropertyValue": "C:/_eNETDNC",
                        "PropertyDescription": "eNET Folder"
                    },
                    "3d849d9d-71e5-41eb-93ef-b1b8edcebbd5": {
                        "PropertyId": "3d849d9d-71e5-41eb-93ef-b1b8edcebbd5",
                        "PropertyName": "WebFolder",
                        "PropertyValue": "./wwwroot",
                        "PropertyDescription": "Web App - website's folder"
                    },
                    "3d849d9d-71e5-41eb-93ef-b1b8edcebbd6": {
                        "PropertyId": "3d849d9d-71e5-41eb-93ef-b1b8edcebbd6",
                        "PropertyName": "WebPort",
                        "PropertyValue": 8030,
                        "PropertyDescription": "Web App - website's port"
                    }
                }
            }
        }
    }
    ```

### Step 4: Set Up the eNETDNC Simulator

1. **Extract** the eNETDNC simulator to the folder: `C:/_eNETDNC`.

### Step 5: Update the Configuration File

1. **Open** the `CSIFLEX.config.json` file created in step 3.
2. **Update** the `IPAddress` in the JSON with your local IP address.
3. **In the `Modules` section**, update the `IpAddress` field under `CSIFLEX_WebApp` to match your local IP.

### Step 6: Configure the Angular Project

1. **Install Node.js** version 14.

2. Open the directory `CSIFLEXSolution/CSIFLEX.Solution/CSIFLEXMobileApp` in Visual Studio Code.

3. In the VS Code terminal, run the following commands to set up the Angular project:

    ```bash
    npm install -g @angular/cli@12.2.0
    npm install
    ```

4. To run the Angular project and solve old version problem, use the following command:

    ```bash
    node --openssl-legacy-provider ./node_modules/@angular/cli/bin/ng serve
    ```

### Step 7: Run the Web API

1. Stop the service that was installed by the CSIFlex server.
2. Set the service to "manual" execution in the Windows Services.
3. Open the `CSIFlex.WebApp.Service` project in Visual Studio and run the application in Debug mode.
4. The Web API will run on the IP address of your machine, on port `8031`.

### Step 8: Configure the `eNETSimulator.dll.config` File

1. Update the IP addresses in the `eNETSimulator.dll.config` file, located in the folder c:/eNETDNC

Here’s the translated and improved version of **Step 9** in English:

### Step 9: Run the eNETDNC Simulator

- Update the IP addresses in the `eNETSimulator.dll.config` file located in the folder `C:/eNETDNC/`.
- Run the `eNETSimulator.exe` file.
- Click the **Start** button next to the **API/WebSocket** option.
- Then, click the **Start** button for the simulator.