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
<link href="PosStyle.css" rel="stylesheet" type="text/css" />
</head>

<body>
<?php if (login_check($mysqli) == true) : ?>
<div id="LeftWhiteSpace">
</div>
<table width="75%" align="center" >
<tr align="center">
        <td><a href="Index.php">Home</a></td>
        <td><a href="Menu.php">Menu</a></td>
        <td><a href="Contact.php">Contact</a></td>
        <td><a href="Reserve.php">Reserve</td>
        <td><a href="includes/logout.php">Logout</td>
    </tr>
</table>


<div id="RightWhiteSpace">
</div>

<div id="TopSpace">
</div>
<div id="TextHome">
  <h1>Reservation successful!</h1>
        <p> Thank You for making reservation in our restaurant</p>
<?php else : ?>

        <div id="LeftWhiteSpace">
        </div>
        <table width="75%" align="center" >
        <tr align="center">
                <td><a href="Index.php">Home</a></td>
                <td><a href="Menu.php">Menu</a></td>
                <td><a href="Contact.php">Contact</a></td>
                <td><a href="Reserve.php">Reserve</td>
                <td><a href="Login.php">Login</td>
            </tr>
        </table>
        
        <div id="RightWhiteSpace">
        </div>
        
        <div id="TopSpace">
        </div>
        <div id="ImageContainerReserve">
        </div>
        
        <div id="TextHome">
        <h1> Please Do Reserve </h1>
        <p> It may cause an inconvenience for us if we can't serve you when you have arrived there, therefore it is best to reserve our seats firstly before coming to our restaurant.<br />
          <br />
          Note: Please be on time, on the reservation, because when it is crowdy, it might be hard to keep the seats empty.</p>
          Please <a href="login.php">login</a> to make a reservation.
        </p>
        </div>
    	<?php endif; ?>
</div>
</body>
</html>