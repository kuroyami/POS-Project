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
<script type="text/JavaScript" src="js/reservation.js"></script> 
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
<div id="ImageContainerReserve">
</div>

<div id="TextHome">
<h1> Please Do Reserve </h1>
<p> It may cause an inconvenience for us if we can't serve you when you have arrived there, therefore it is best to reserve our seats firstly before coming to our restaurant.<br />
  <br />
  Note: Please be on time, on the reservation, because when it is crowdy, it might be hard to keep the seats empty.</p>
<p>Welcome <?php echo htmlentities($_SESSION['username']); ?>!</p>
          <div id="reservation">
          <h1>Reservation</h1>
                    <div>
                    
              <form method="post" action="reserve_submit.php">
                    Personal Information<br/>
                    
                    First Name:
                    <input name="firstname" type="text" size="30" maxlength="20" onblur="validateFirstName(this);" /><label id="errorFirstName" class="errorLabel"></label><br/>
                    

					Last Name:
                    	<input name="lastname" type="text" size="30" maxlength="20" onblur="validateLastName(this);"/><label id="errorLastName" class="errorLabel"></label><br/>
                                                
                    Phone Number:
                        <input name="phone" type="text" size="20" maxlength="15" onblur="validatePhone(this);"/><label id="errorPhone" class="errorLabel"></label><br/>
                    Email Address:
                        <input name="email" type="text" size="50" maxlength="50" onblur="validateEmail(this);"/><label id="errorEmail" class="errorLabel"></label><br/>
                    Reservation Info<br/>
                    Amount of people:
						<input name="people" type="text" size="2" maxlength="2" onblur="validatePeople(this);"/><label id="errorPeople" class="errorLabel"></label><br/>
                    Date (Day/Month/Year):
                        <input name="dates" type="text" size="10" maxlength="10" onblur="validateDate(this);"/><label id="errorDate" class="errorLabel"></label><br/>
                    time (Hours:Minutes):
                        <input name="times" type="text" size="5" maxlength="5" onblur="validateTime(this);"/><label id="errorTime" class="errorLabel"></label><br/>
					comments:
                        <input name="comments" type="text"/><br/>
                          
                    <input name="submit" type="submit" value="SUBMIT" onclick="validateForm(signupform)"/>
                    <input name="reset" type="reset" value="RESET" />
                    </form>
                    </div>
                    </div>
             </div>
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
