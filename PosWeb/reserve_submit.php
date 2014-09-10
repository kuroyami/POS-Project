<?php

if ($_SERVER['REQUEST_METHOD'] == 'POST') 
{

$FName = $_POST['firstname'];
$LName = $_POST['lastname'];
$Email = $_POST['email'];
$Phone = $_POST['phone'];
$Comment = $_POST['comments'];
$People = $_POST['people'];
$Date = $_POST['dates'];
$Time = $_POST['times'];
$Input_comment = $Comment." amount of people ". $People;

//Establish a connection to the Database. must download Microsoft Data Access Components

$conn = odbc_connect('Driver={Microsoft Access Driver (*.mdb, *.accdb)}; 
DBQ=C:\xampp\htdocs\PosWeb\RestaurantDB.accdb','','');

//SQL Statement
$sql = "Insert Into WaitingList (FirstName,LastName,Email,PhoneNum,Comment,ResDate,ResTime) VALUES ('".$FName."','".$LName."','".$Email."','".$Phone."','".$Input_comment."','".$Date."','".$Time."')";
	
//Execute SQL Statement and store results as a recordset
	
$rs = @odbc_exec($conn,$sql);

if (!$rs)
{

header('Location: ../PosWeb/error.php');


}

else

{
header('Location: ../PosWeb/reservation_success.php');

}

odbc_close($conn);

}

?>;

}

?>