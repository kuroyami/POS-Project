<?php
include_once 'includes/db_connect.php';
include_once 'includes/functions.php';
 
sec_session_start();
?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>PosWeb</title>
<link href='http://fonts.googleapis.com/css?family=Josefin+Slab' rel='stylesheet' type='text/css'>
<link href="PosStyle.css" rel="stylesheet" type="text/css" />

</head>

<body>

<div id="LeftWhiteSpace">
</div>
<?php if (login_check($mysqli) == true) : ?>
<table width="75%" align="center" >
<tr align="center">
        <td><a href="Index.php">Home</a></td>
        <td><a href="Menu.php">Menu</a></td>
        <td><a href="Contact.php">Contact</a></td>
        <td><a href="Reserve.php">Reserve</td>
        <td><a href="includes/logout.php">Logout</td>
    </tr>
</table>
<?php else : ?>
<table width="75%" align="center" >
<tr align="center">
        <td><a href="Index.php">Home</a></td>
        <td><a href="Menu.php">Menu</a></td>
        <td><a href="Contact.php">Contact</a></td>
        <td><a href="Reserve.php">Reserve</td>
        <td><a href="Login.php">Login</td>
    </tr>
</table>
<?php endif; ?>

<div id="RightWhiteSpace">
</div>

<div id="TopSpace">
</div>
<div id="ImageContainerMenu">
</div>

<div id="TextHome">
<h1> Top Choices! </h1>
<p> We serve best sandwiches in our town! With many kind of bread, warmly baked and best meats that grilled in perfect temperature it makes a <b> Crunchy </b> taste in mouth! </p>
</div>


</body>
</html>
