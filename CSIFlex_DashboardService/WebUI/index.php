<<<<<<< HEAD
<?php

ob_start();
// header('Content-Length: '.ob_get_length());
$servername = "10.0.10.189";
$username = "root";
$password = "CSIF1337";
$dbname = "csi_dashboard";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT machine_name_,current_status_,machine_part_no_,part_count_,SEC_TO_TIME(last_cycle_time) as last_cycle_time, SEC_TO_TIME(TIMESTAMPDIFF(SECOND,correction_date_,now())) as current_cycle_time, SEC_TO_TIME(TIMESTAMPDIFF(SECOND,correction_date_,now())) as elapsed_time, shift_name_ FROM csi_dashboard.tbl_monsetup_data WHERE mon_state_='1';";
$result = $conn->query($sql);

$conn->close();

function getStatusText($current_status)
{
	if ($current_status == "_CON")
	{
		return "CYCLE ON";
	}
	else if ($current_status == "_COFF")
	{
		return "CYCLE OFF";
	}
	else if ($current_status == "_SETUP")
	{
		return "SETUP";
	}
	else if ($current_status == "")
	{
		return "NO eMONITOR";
	}
	else{
		return $current_status;
	}
}

?>
<html><head>
<script src="http://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            
            //$('#gridData').html('<tr bgcolor="#CCCCCC"><th width="157" scope="col">Machine Name</th><th width="153" scope="col">Status</th><th width="153" scope="col">Part Number</th><th width="100" scope="col">Cycle Count</th><th width="140" scope="col">Last Cycle</th><th width="140" scope="col">Current Cycle</th><th width="140" scope="col">Elapsed Time</th></tr>');

            getData();
            
        });
        var isFirstTime = true;
	    var dashboardApi = 'http://10.0.10.189:82/dashboard/dashboard_api.php';
		//var dashboardApi = 'http://localhost:82/dashboard/dashboard_api.php';
		function getStatusText(current_status){
			if (current_status == "_CON")
			{
				return "CYCLE ON";
			}
			else if (current_status == "_COFF")
			{
				return "CYCLE OFF";
			}
			else if (current_status == "_SETUP")
			{
				return "SETUP";
			}
			else if (current_status == "")
			{
				return "NO eMONITOR";
			}
			else{
				return current_status;
			}
		}
		
        function getData() {
            setTimeout(getData, 1000);
            $.ajax(dashboardApi).success(function (data) {
                console.log(data);
                var response = JSON.parse(data);
                var gridData = $('#gridData');
				var j = 2;
                for (var i = 0; i < response.length; i++) {
                    // if (isFirstTime) {
                        // gridData.append('<tr id="' + i + '" align="center"><td bgcolor="#0033FF"><font color="#FFFFFF">' + response[i].machine_name_ + '<LI>' + (i + 1) + '</font></td><td bgcolor="#00FFFF"><font color="#0">' + response[i].current_status_ + '</font></td><td bgcolor="#0033FF"><font color="#FFFFFF">' + response[i].machine_part_no_ + '</font></td><td bgcolor="#0033FF"><font color="#FFFFFF">' + response[i].part_count_ + '</font></td><td bgcolor="#FFFF00"><font color="#0">' + response[i].last_cycle_time + '</font></td><td bgcolor="#FFFF00"><font color="#0">' + response[i].current_cycle_time + '</font></td><td bgcolor="#0033FF"><font color="#FFFFFF">' + response[i].elapsed_time + '</font></td></tr>');
                    // }
                    // else {			
					var shift_name = '0';
					if(response[i].shift_name_ != ''){
						shift_name = response[i].shift_name_;
					}
					var tr = gridData.find("tr:eq('" + j + "')");
					tr.find("td:eq(0)").html('<font color="#FFFFFF">' + response[i].machine_name_ + '<LI>' + shift_name.replace("Shift ", "") + '</font>');
					tr.find("td:eq(1)").html('<font color="#0">' + getStatusText(response[i].current_status_) + '</font>');
					tr.find("td:eq(2)").html('<font color="#FFFFFF">' + response[i].machine_part_no_ + '</font>');
					tr.find("td:eq(3)").html('<font color="#FFFFFF">' + response[i].part_count_ + '</font>');
					tr.find("td:eq(4)").html('<font color="#0">' + response[i].last_cycle_time + '</font>');
					tr.find("td:eq(5)").html('<font color="#0">' + response[i].current_cycle_time + '</font>');
					tr.find("td:eq(6)").html('<font color="#FFFFFF">' + response[i].elapsed_time + '</font>');
                    j++;
					// }
						
                }
                isFirstTime = false;
            });
        }

    </script>
<style type="text/css"><!--.Large {font-size: xx-large;}.red {color: #F00;}--></style>
<body><body bgcolor='#6699CC'><div align="center">
<table width="1018" height="50" border="0">
<tr><th height="50" colspan="7" bgcolor="#6699CC" class="Large" scope="col">- <span class="red">eNet</span>DNC Machine Monitoring -</th></tr>
</table></div>
<table width="300" height="30" border="0" align="center">
<tr><th height="30" colspan="7" bgcolor="#6699CC" scope="col">
<form method="GET">
<p> <input type="SUBMIT" value="Refresh" name="Refresh"> <input type="SUBMIT" value="Start AutoRefresh" name="Start"> </p>
</form>
<form action="index.php" method="POST">
<p><input type="SUBMIT" value="Continue"></p><br>
</form>
</th></tr></table>
<div align="center"><table id="gridData" width="1018" border="2"><tr bgcolor="#CCCCCC">
<th width="157" scope="col">Machine Name</th>
<th width="153" scope="col">Status</th>
<th width="153" scope="col">Part Number</th>
<th width="100" scope="col">Cycle Count</th>
<th width="140" scope="col">Last Cycle</th>
<th width="140" scope="col">Current Cycle</th>
<th width="140" scope="col">Elapsed Time</th>
</tr>
<tr align="center"> <th colspan="7" bgcolor="#6699CC" class="Medium" scope="col">SWISS_TURN</th></tr>
<?php
$total_rows = $result->num_rows;
$i=0;
while($row = $result->fetch_assoc()) {
?>
<tr align="center"><td bgcolor="#0033FF"><font color='#FFFFFF'><?php echo $row["machine_name_"];?><LI><?php echo ($row["shift_name_"] != ''? str_replace("Shift ", "" ,$row["shift_name_"]) : "0");?></font></td><td bgcolor='#00FFFF'><font color='#0'><?php echo getStatusText($row["current_status_"]);?></font></td><td bgcolor="#0033FF"><font color='#FFFFFF'><?php echo $row["machine_part_no_"];?></font></td><td bgcolor="#0033FF"><font color='#FFFFFF'><?php echo $row["part_count_"];?></font></td><td bgcolor='#FFFF00'><font color='#0'><?php echo $row["last_cycle_time"];?></font></td><td bgcolor='#FFFF00'><font color='#0'><?php echo $row["current_cycle_time"];?></font></td><td bgcolor="#0033FF"><font color='#FFFFFF'><?php echo $row["elapsed_time"];?></font></td>
</tr>
<?php
$i++;
if($i == $total_rows){
	header('Content-Length: '.ob_get_length());
}
}

?>
</table></div></body></html>
<?php 
ob_end_flush();

=======
<?php

ob_start();
// header('Content-Length: '.ob_get_length());
$servername = "10.0.10.189";
$username = "root";
$password = "CSIF1337";
$dbname = "csi_dashboard";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT machine_name_,current_status_,machine_part_no_,part_count_,SEC_TO_TIME(last_cycle_time) as last_cycle_time, SEC_TO_TIME(TIMESTAMPDIFF(SECOND,correction_date_,now())) as current_cycle_time, SEC_TO_TIME(TIMESTAMPDIFF(SECOND,correction_date_,now())) as elapsed_time, shift_name_ FROM csi_dashboard.tbl_monsetup_data WHERE mon_state_='1';";
$result = $conn->query($sql);

$conn->close();

function getStatusText($current_status)
{
	if ($current_status == "_CON")
	{
		return "CYCLE ON";
	}
	else if ($current_status == "_COFF")
	{
		return "CYCLE OFF";
	}
	else if ($current_status == "_SETUP")
	{
		return "SETUP";
	}
	else if ($current_status == "")
	{
		return "NO eMONITOR";
	}
	else{
		return $current_status;
	}
}
function getCurrentCycle($row)
{
	if ($row["current_status_"] != "_CON")
	{
		return "00:00:00";
	}
	else{
		return $row["current_cycle_time"];
	}
	
}
function getOperatorId($row)
{
	$word = "OID:";
	if(strpos($row["machine_part_no_"],$word)!==false)
	{
		$row["machine_part_no_"]=str_replace($word,"<LI>",$row["machine_part_no_"]);
		return $row["machine_part_no_"];
	}
	else
	{
		return $row["machine_part_no_"];
	}
	
}
?>
<html><head>
<script src="http://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            
            //$('#gridData').html('<tr bgcolor="#CCCCCC"><th width="157" scope="col">Machine Name</th><th width="153" scope="col">Status</th><th width="153" scope="col">Part Number</th><th width="100" scope="col">Cycle Count</th><th width="140" scope="col">Last Cycle</th><th width="140" scope="col">Current Cycle</th><th width="140" scope="col">Elapsed Time</th></tr>');

            getData();
            
        });
        var isFirstTime = true;
	    var dashboardApi = 'http://10.0.10.189:82/dashboard/dashboard_api.php';
		//var dashboardApi = 'http://localhost:82/dashboard/dashboard_api.php';
		function getStatusText(current_status){
			if (current_status == "_CON")
			{
				return "CYCLE ON";
			}
			else if (current_status == "_COFF")
			{
				return "CYCLE OFF";
			}
			else if (current_status == "_SETUP")
			{
				return "SETUP";
			}
			else if (current_status == "")
			{
				return "NO eMONITOR";
			}
			else{
				return current_status;
			}
		}
		function getCurrentCycle(row)
		{
			if (row.current_status_ != "_CON")
			{
				return "00:00:00";
			}
			else{
				return row.current_cycle_time;
			}
			
		}
		function getOperatorId(row)
		{
			var str = row.machine_part_no_;
			var word = "OID:";
			if(str.indexOf(word)!==-1)
			{
				str= str.replace(word,"<LI>");
				console.log(str);
				return str;
			}
			else
			{
				return str;
			}
			
		}
        function getData() {
            setTimeout(getData, 1000);
            $.ajax(dashboardApi).success(function (data) {
                //console.log(data);
                var response = JSON.parse(data);
                var gridData = $('#gridData');
				var j = 2;
                for (var i = 0; i < response.length; i++) {
                    // if (isFirstTime) {
                        // gridData.append('<tr id="' + i + '" align="center"><td bgcolor="#0033FF"><font color="#FFFFFF">' + response[i].machine_name_ + '<LI>' + (i + 1) + '</font></td><td bgcolor="#00FFFF"><font color="#0">' + response[i].current_status_ + '</font></td><td bgcolor="#0033FF"><font color="#FFFFFF">' + response[i].machine_part_no_ + '</font></td><td bgcolor="#0033FF"><font color="#FFFFFF">' + response[i].part_count_ + '</font></td><td bgcolor="#FFFF00"><font color="#0">' + response[i].last_cycle_time + '</font></td><td bgcolor="#FFFF00"><font color="#0">' + response[i].current_cycle_time + '</font></td><td bgcolor="#0033FF"><font color="#FFFFFF">' + response[i].elapsed_time + '</font></td></tr>');
                    // }
                    // else {			
					var shift_name = '0';
					if(response[i].shift_name_ != ''){
						shift_name = response[i].shift_name_;
					}
					var tr = gridData.find("tr:eq('" + j + "')");
					tr.find("td:eq(0)").html('<font color="#FFFFFF">' + response[i].machine_name_ + '<LI>' + shift_name.replace("Shift ", "") + '</font>');
					tr.find("td:eq(1)").html('<font color="#0">' + getStatusText(response[i].current_status_) + '</font>');
					tr.find("td:eq(2)").html('<font color="#FFFFFF">' + getOperatorId(response[i])+ '</font>');
					tr.find("td:eq(3)").html('<font color="#FFFFFF">' + response[i].part_count_ + '</font>');
					tr.find("td:eq(4)").html('<font color="#0">' + response[i].last_cycle_time + '</font>');
					tr.find("td:eq(5)").html('<font color="#0">' +getCurrentCycle(response[i])+ '</font>');
					tr.find("td:eq(6)").html('<font color="#FFFFFF">' + response[i].elapsed_time + '</font>');
                    j++;
					// }
						
                }
                isFirstTime = false;
            });
        }

    </script>
<style type="text/css"><!--.Large {font-size: xx-large;}.red {color: #F00;}--></style>
<body><body bgcolor='#6699CC'><div align="center">
<table width="1018" height="50" border="0">
<tr><th height="50" colspan="7" bgcolor="#6699CC" class="Large" scope="col">- <span class="red">eNet</span>DNC Machine Monitoring -</th></tr>
</table></div>
<table width="300" height="30" border="0" align="center">
<tr><th height="30" colspan="7" bgcolor="#6699CC" scope="col">
<form method="GET">
<p> <input type="SUBMIT" value="Refresh" name="Refresh"> <input type="SUBMIT" value="Start AutoRefresh" name="Start"> </p>
</form>
<form action="index.php" method="POST">
<p><input type="SUBMIT" value="Continue"></p><br>
</form>
</th></tr></table>
<div align="center"><table id="gridData" width="1018" border="2"><tr bgcolor="#CCCCCC">
<th width="157" scope="col">Machine Name</th>
<th width="153" scope="col">Status</th>
<th width="153" scope="col">Part Number</th>
<th width="100" scope="col">Cycle Count</th>
<th width="140" scope="col">Last Cycle</th>
<th width="140" scope="col">Current Cycle</th>
<th width="140" scope="col">Elapsed Time</th>
</tr>
<tr align="center"> <th colspan="7" bgcolor="#6699CC" class="Medium" scope="col">SWISS_TURN</th></tr>
<?php
$total_rows = $result->num_rows;
$i=0;
while($row = $result->fetch_assoc()) {
?>
<tr align="center"><td bgcolor="#0033FF"><font color='#FFFFFF'><?php echo $row["machine_name_"];?><LI><?php echo ($row["shift_name_"] != ''? str_replace("Shift ", "" ,$row["shift_name_"]) : "0");?></font></td><td bgcolor='#00FFFF'><font color='#0'><?php echo getStatusText($row["current_status_"]);?></font></td><td bgcolor="#0033FF"><font color='#FFFFFF'><?php echo getOperatorId($row);?></font></td><td bgcolor="#0033FF"><font color='#FFFFFF'><?php echo $row["part_count_"];?></font></td><td bgcolor='#FFFF00'><font color='#0'><?php echo $row["last_cycle_time"];?></font></td><td bgcolor='#FFFF00'><font color='#0'><?php echo getCurrentCycle($row);?></font></td><td bgcolor="#0033FF"><font color='#FFFFFF'><?php echo $row["elapsed_time"];?></font></td>
</tr>
<?php
$i++;
if($i == $total_rows){
	header('Content-Length: '.ob_get_length());
}
}

?>
</table></div></body></html>
<?php 
ob_end_flush();

>>>>>>> 538eda2a66e421e02e674fb005c79586bfc39e28
?>