<<<<<<< HEAD
<?php
header("Access-Control-Allow-Origin: *");
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
// $result = $conn->query($sql);
// print_r($result);
$result = $conn->query($sql);

$conn->close();
$return_val = array();
$i = 0;
while($row = $result->fetch_assoc()) {
	$return_val[$i] = $row;
	$i++;
}
echo(json_encode($return_val));
=======
<?php
header("Access-Control-Allow-Origin: *");
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
// $result = $conn->query($sql);
// print_r($result);
$result = $conn->query($sql);

$conn->close();
$return_val = array();
$i = 0;
while($row = $result->fetch_assoc()) {
	$return_val[$i] = $row;
	$i++;
}
echo(json_encode($return_val));
>>>>>>> 538eda2a66e421e02e674fb005c79586bfc39e28
?>