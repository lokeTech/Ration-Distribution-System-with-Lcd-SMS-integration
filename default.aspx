<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="BiometricRationDistribution._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title></title>
    <style>
        body {
            margin: 0;
            font-family: Arial, sans-serif;
        }

        /* Header */
        header {
            background: #2c3e50;
            color: white;
            padding: 15px 20px;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

            header h1 {
                margin: 0;
                font-size: 22px;
            }

        nav a {
            color: white;
            text-decoration: none;
            margin-left: 20px;
            font-size: 16px;
        }

            nav a:hover {
                text-decoration: underline;
            }

        /* Main layout */
        .container {
            display: flex;
            height: calc(100vh - 60px); /* full height minus header */
        }

        /* Left half with image */
        .left {
            flex: 1;
            background: url('img/grains.jpg') no-repeat center center/cover;
        }

        /* Right half with login form */
        .right {
            flex: 1;
            display: flex;
            justify-content: center;
            align-items: center;
            background: #ecf0f1;
        }

        .login-box {
            background: white;
            padding: 40px;
            border-radius: 10px;
            box-shadow: 0px 4px 12px rgba(0, 0, 0, 0.1);
            width: 80%;
            max-width: 400px;
        }

            .login-box h2 {
                margin-bottom: 20px;
                text-align: center;
                color: #2c3e50;
            }

            .login-box input {
                width: 100%;
                padding: 12px;
                margin: 10px 0;
                border: 1px solid #ccc;
                border-radius: 6px;
                font-size: 14px;
            }

            .login-box button {
                width: 100%;
                padding: 12px;
                background: #2c3e50;
                color: white;
                border: none;
                border-radius: 6px;
                font-size: 16px;
                cursor: pointer;
            }

                .login-box button:hover {
                    background: #1a242f;
                }

        /* Responsive Design */
        @media (max-width: 768px) {
            .container {
                flex-direction: column;
                height: auto;
            }

            .left {
                height: 200px; /* smaller height for mobile */
            }

            .right {
                padding: 20px;
            }
        }
        /* Style for ASP.NET button */
        .login-box input[type="submit"],
        .login-box .aspNetButton {
            width: 100%;
            padding: 12px;
            background: #2c3e50;
            color: white;
            border: none;
            border-radius: 6px;
            font-size: 16px;
            cursor: pointer;
            transition: background 0.3s ease;
        }

            .login-box input[type="submit"]:hover,
            .login-box .aspNetButton:hover {
                background: #1a242f;
            }
    </style>
</head>
<body>
    <!-- Header -->
    <header>
        <h1>Biometric ration distribution system</h1>
        <nav>
            <a href="#">Home</a>
            <a href="#">About</a>
            <a href="#">Contact</a>
        </nav>
    </header>

    <!-- Main Content -->
    <div class="container">
        <!-- Left Half (Image) -->
        <div class="left"></div>

        <!-- Right Half (Login) -->
        <div class="right">
            <div class="login-box">
                <h2>Login</h2>
                <form id="form1" runat="server">
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="input" Text="RC100001"
                        Placeholder="Ration Card No." required></asp:TextBox>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="input"
                        TextMode="Password" Placeholder="Password" required></asp:TextBox>
                    <asp:Button ID="btnLogin" runat="server"
                        Text="Login"
                        CssClass="aspNetButton"
                        OnClick="btnLogin_Click" />
                    <br />
                    <asp:Label ID="lblMessage" runat="server" Text="Label"></asp:Label>
                </form>

            </div>
        </div>
    </div>
    <div>
    </div>
</body>
</html>
