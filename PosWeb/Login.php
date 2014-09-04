<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>PosWeb</title>
<link href='http://fonts.googleapis.com/css?family=Josefin+Slab' rel='stylesheet' type='text/css'>
<link href="PosStyle.css" rel="stylesheet" type="text/css" />
 	<script type="text/JavaScript" src="js/sha512.js"></script> 
    <script type="text/JavaScript" src="js/forms.js"></script> 
</head>

<body>

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
<div id="ImageContainerLogin">
</div>

<?php
include_once 'includes/register.inc.php';
include_once 'includes/functions.php';
include_once 'includes/db_connect.php'; 
sec_session_start();
?>
<div id="TextHome">
<h1> Login</h1><br />

<form action="includes/process_login.php" method="post" name="login_form">                      
    	<label>Email:</label>
        <input type="text" name="email" />
        <label>Password:</label>
        <input type="password" name="password" id="password"/>
        </div>
        <input type="button" value="Login" onclick="formhash(this.form, this.form.password);" /> 
    </form>
<br />
</div>
<div id="SignUp">
<h1 id="H1FontStyled"> Sign Up</h1><br />
<form action="<?php echo esc_url($_SERVER['PHP_SELF']); ?>" method="post" name="registration_form">
			<label>Username:</label>
            <input type='text' name='username' id='username' /><br>
            <label>Email:</label>
            <input type="text" name="email" id="email" /><br>
            <label>Password:</label>
            <input type="password"name="password" id="password"/><br>
            <label>Confirm password:</label>
            <input type="password" name="confirmpwd" id="confirmpwd" /><br>
        </div>
            <input type="button" value="Register" 
                   onclick="return regformhash(this.form,
                                   this.form.username,
                                   this.form.email,
                                   this.form.password,
                                   this.form.confirmpwd);" /> 
           	<input name="reset" type="reset" value="RESET" />
  </form>
</div>


</body>
</html>
