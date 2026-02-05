<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CSIFlex_Dashboard.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head title="CSIFlex Dashboard">
    <style type="text/css">
        .Large {
            font-size: xx-large;
        }

        .red {
            color: #F00;
        }

        th {
            text-align: center !important;
            font: bolder 14px !important;
            font-weight: 700 !important;
            color: black !important;
            border-color: black; /*#e4eaec*/
            border-width: 3px;
            height: 40px;
            background-color: lightgray; /*#5CD29D*/
        }

        td {
            text-align: center !important;
            font: bolder 14px !important;
            font-weight: 700 !important;
            color: black !important;
            background-color: white; /*#57c7d4*/
            border-color: black !important; /*#e4eaec*/
            border-width: 3px;
        }

        .border tr td {
            border: 2px solid black;
            height: 40px;
        }

        .overflow {
            overflow-y: scroll !important; /*max-width: 500px !important; font-size:medium;*/
        }
    </style>
    <script type="text/javascript">
        //function load() {
        //setTimeout("window.location.reload(true);", 4000);
        window.setInterval(function () {
            // this will execute every 1 second

            methodCallOrAction();
        }, 1000);
        function methodCallOrAction() {
            window.location.reload(true);
        }
        //}
    </script>
</head>
<body bgcolor='#6699CC'>
    <div align="center">
        <table width="1018" height="50" border="0">
            <tr>
                <th height="50" colspan="7" bgcolor="#6699CC" class="Large" scope="col" style="background-color: #6699CC !important;">- <span class="red">CSIFlex </span>Dashboard-</th>
            </tr>
        </table>
    </div>
    <%--<table width="1018" height="50" border="0">
            <tr>
                <th height="50" colspan="7" bgcolor="#6699CC" class="Large" scope="col">- <span class="red">CSIFlex </span>Dashboard-</th>
            </tr>
        </table>
    </div>
    <table width="300" height="30" border="0" align="center">
        <tr>
            <th height="30" colspan="7" bgcolor="#6699CC" scope="col">
                <form method="GET">
                    <p>
                        <input type="SUBMIT" value="Refresh" name="Refresh">
                        <input type="SUBMIT" value="Start AutoRefresh" name="Start">
                    </p>
                </form>
            </th>
        </tr>
    </table>--%>
    <form id="form1" runat="server">
        <div align="center">
            <asp:Label ID="ErrorDis" Visible="false" runat="server"></asp:Label>
            <%--<asp:Button ID="btnBind" runat="server" Text="Auto Refresh" OnClientClick="load();"  />--%>
            <%--<asp:button id="Button1" runat="server" text="Stop Refresh" onclientclick="return false;" autopostback="false" xmlns:asp="#unknown" />--%>
            <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ></asp:SqlDataSource>--%>
            <asp:GridView ID="GridView1" runat="server"
                BackColor="LightGoldenrodYellow" BorderColor="Black"
                BorderWidth="2px" CellPadding="2" CssClass="border"
                ForeColor="Black" GridLines="None" Height="80%"
                Width="80%" AutoGenerateColumns="false" OnRowDataBound = "GridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Machine Name" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="2px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="th">
                        <ItemTemplate>
                            <asp:Label runat="server" CssClass="overflow" ID="lblMachineName" Text='<%# Eval("machine_name_") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="2px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="th">
                        <ItemTemplate>
                            <asp:Label runat="server" CssClass="overflow" ID="lblStatus" Text='<%# Eval("current_status_") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Part Number" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="2px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="th">
                        <ItemTemplate>
                            <asp:Label ID="lblPartNumber" runat="server" CssClass="overflow" Text='<%# Eval("machine_part_no_") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cycle Count" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="2px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="th">
                        <ItemTemplate>
                            <asp:Label ID="lblCycleCount" runat="server" CssClass="overflow" Text='<%# Eval("part_count_") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Last Cycle Time" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="2px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="th">
                        <ItemTemplate>
                            <asp:Label ID="lblLastCycle" runat="server" CssClass="overflow" Text='<%# Eval("last_cycle_time") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Current Cycle Time" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="2px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="th">
                        <ItemTemplate>
                            <asp:Label ID="lblCurrentCycle" runat="server" CssClass="overflow" Text='<%# Eval("current_cycle_time") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Elapsed Time" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="2px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="th">
                        <ItemTemplate>
                            <asp:Label ID="lblElapsedTime" runat="server" CssClass="overflow" Text='<%# Eval("elapsed_time")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                </Columns>
                <%-- <RowStyle Height="40px;"/>--%>
            </asp:GridView>

        </div>
        <%--<asp:Timer ID="Timer1" runat="server" Interval="1000" ontick="Timer1_Tick" />--%>
        <%--<AlternatingRowStyle BackColor="PaleGoldenrod" />
                <FooterStyle BackColor="Tan" />
                <HeaderStyle BackColor="Tan" Font-Bold="True" />
                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue"
                    HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                <SortedAscendingCellStyle BackColor="#FAFAE7" />
                <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                <SortedDescendingCellStyle BackColor="#E1DB9C" />
                <SortedDescendingHeaderStyle BackColor="#C2A47B" />--%>
    </form>
</body>
<%--<head runat="server">
    <title>CSIFlex Dashboard</title>
    <style type="text/css">
        body {
            background-color: cornflowerblue;
        }

        th {
            background-color: gold;
            /*font : 5px solid black;
            font-weight:600;*/
            font-family: Arial;
            font-size: large;
        }

        table {
            width: 100%;
            border-style: solid;
        }

        td {
            text-align: center;
            background-color: darkturquoise;
            font-family: Impact, Haettenschweiler, 'Arial Narrow Bold', sans-serif;
        }

        tr {
            border-color: black;
            border-width: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
    <table border="1px bold black;">
        <thead>
            <tr>
                <th>Machine Name
                </th>
                <th>Current Status
                </th>
                <th>Part Number
                </th>
                <th>Count
                </th>
                <th>Last Cycle
                </th>
                <th>Current Cycle
                </th>
                <th>Elapsed Time
                </th>
            </tr>
        </thead>
        <tr>
            <td>CMTS-1
            </td>
            <td>CYCLE ON
            </td>
            <td>123456
            </td>
            <td>2</td>
            <td>00:00:00
            </td>
            <td>00:05:30
            </td>
            <td>00:05:30
            </td>
        </tr>
    </table>
    
      
    </>
</body>--%>
</html>

