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
<script type="text/JavaScript" src="js/dropdowndate.js"></script> 
</head>

<body>

<div id="LeftWhiteSpace">
</div>
<table width="75%" align="center" >
<tr align="center">
        <td><a href="Index.html">Home</a></td>
        <td><a href="Menu.html">Menu</a></td>
        <td><a href="Contact.html">Contact</a></td>
        <td><a href="Reserve.php">Reserve</td>
        <td><a href="Login.php">Login</td>
    </tr>
</table>

<div id="RightWhiteSpace">
</div>

<div id="ImageContainer">
</div>

<div id="TextHome">
<h1> Please Do Reserve </h1>
<p> It may cause an inconvenience for us if we can't serve you when you have arrived there, therefore it is best to reserve our seats firstly before coming to our restaurant.<br />
  <br />
  Note: Please be on time, on the reservation, because when it is crowdy, it might be hard to keep the seats empty.</p>
      <?php if (login_check($mysqli) == true) : ?>
          <p>Welcome <?php echo htmlentities($_SESSION['username']); ?>!</p>
          <div id="reservation">
          <h1>Reservation</h1>
                    <div>
                        <form action="">
                        Name: <input type="text" name="name"><br>
                        Amount of people: 
                        <select name="people">
                        <option value="1">1</option>
                        <option value="2">2</option>
                        <option value="3">3</option>
                        <option value="4">4</option>
                        <option value="5">5</option>
                        <option value="6">6</option>
                        <option value="7">7</option>
                        <option value="8">8</option>
                        <option value="9">9</option>
                        <option value="10">10</option>
                        </select><br>
                        Date:
                        <select id="daydropdown">
                        </select> 
                        <select id="monthdropdown">
                        </select> 
                        <select id="yeardropdown">
                        </select> 
                        </form>
                        <input type="submit" value="Submit">
        
                        <script type="text/javascript">
        
                        //populatedropdown(id_of_day_select, id_of_month_select, id_of_year_select)
                        window.onload=function(){
                        populatedropdown("daydropdown", "monthdropdown", "yeardropdown")
                        }
                        </script>
                    </div>
        <?php else : ?>
        <p>
          Please <a href="login.php">login</a> to make a reservation.
        </p>
    	<?php endif; ?>
</div>


</body>
</html>
