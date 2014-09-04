<?php
$dbName = $_SERVER["DOCUMENT_ROOT"] . "PosWeb\RestaurantDB.accdb";
if (!file_exists($dbName)) {
    die("Could not find database file.");
}
$db = new PDO("odbc:DRIVER={Microsoft Access Driver (*.mdb)}; DBQ=$dbName; Uid=; Pwd=;");

$sql  = "INSERT INTO waitingList";
$sql .= "       (FirstName, LastName, Email, PhoneNum, Comment, ResDate, ResTime) ";
$sql .= "VALUES (" .$_REQUEST['firstname']. ", " . $_REQUEST['lastname'] . ", " . $_REQUEST['email'] . ", " . $_REQUEST['phone'] . ", " . $_REQUEST['comments'] . ", " . $_REQUEST['people'] . ", " . $_REQUEST['date'] . ", " . $_REQUEST['time'] . ")";
 
$db->query($sql);
	
?>