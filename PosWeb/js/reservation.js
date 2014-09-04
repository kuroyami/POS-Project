// JavaScript Document
function validateForm(theForm){	
	var valFName = validateFirstName(theForm.firstname);
	var valLName = validateLastName(theForm.lastname);
	var valPhone = validatePhone(theForm.phone);
	var valEmail = validateEmail(theForm.email);
	var valPeople = validatePeople(theForm.people);
	var valDate = validateDate(theForm.date);
	var valTime = validateTime(theForm.time);
	
	if(valFName && valLName && valAge && valPhone && valEmail&&valPeople && valDate && valTime){ 
			$.ajax({
				type:"POST",
				url:"sign_up.php",
				data:{"firstname":theForm.firstname.value, "lastname":theForm.lastname.value, "phone":theForm.phone.value, "email":theForm.email.value,"people":theForm.people.value, "date":theForm.date.value,"date":theForm.time.value},
				dataType: "json",
				success: function(response){
					alert(response);
				}
			});
	}
}
function validateFirstName(field){
	var illegalChars = /[0-9]/;	
	  
	if (field.value == "") {
		field.style.background = 'Yellow'; 
		document.getElementById("errorFirstName").innerHTML = "You didn't enter your first name"
		return false;
	} else if (illegalChars.test(field.value)) {
		document.getElementById("errorFirstName").innerHTML = "The name contains illegal characters";
		field.style.background = 'Yellow';
		return false;
	} else {
		field.style.background = 'White';
		document.getElementById("errorFirstName").innerHTML = "";
		return true;
	}
}

function validateLastName(field){
	var illegalChars = /[0-9]/;
					  
	if (field.value == "") {
		field.style.background = 'Yellow'; 
		document.getElementById("errorLastName").innerHTML = "You didn't enter your last name"
		return false;
	} else if (illegalChars.test(field.value)) {
		document.getElementById("errorLastName").innerHTML = "The name contains illegal characters";
		field.style.background = 'Yellow';
		return false;
	} else {
		field.style.background = 'White';
		document.getElementById("errorLastName").innerHTML = "";
		return true;
	}
}


function validatePhone(field){
	var stripped = field.value.replace(/[\(\)\.\-\ ]/g, '');     

	if (field.value == "") {
		document.getElementById("errorPhone").innerHTML = "You didn't enter a phone number";
		field.style.background = 'Yellow';
		return false;
	} else if (isNaN(parseInt(stripped))) {
		document.getElementById("errorPhone").innerHTML = "The phone number contains illegal characters";
		field.style.background = 'Yellow';
		return false;
	} else if (stripped.length > 10) {
		document.getElementById("errorPhone").innerHTML = "The phone number exceeds the length limit";
		field.style.background = 'Yellow';
		return false;
	} else{
		field.style.background = 'White';
		document.getElementById("errorPhone").innerHTML = "";
		return true;
	}
}

function validateEmail(field){
	var atPos = field.value.indexOf("@");
	var dotPos = field.value.lastIndexOf(".");
	var valid = (atPos > 0) && (dotPos > atPos + 1) && (field.value.length > dotPos + 2);
	var illegalChars = /[\(\)\<\>\,\;\:\\\"\[\]]/ ;
	
	if (field.value == "") {
		field.style.background = 'Yellow';
		document.getElementById("errorEmail").innerHTML = "You didn't enter an email address";
		return false;
	} else if (!valid) {
		field.style.background = 'Yellow';
		document.getElementById("errorEmail").innerHTML = "Please enter a valid email address";
		return false;
	} else if (field.value.match(illegalChars)) {
		field.style.background = 'Yellow';
		document.getElementById("errorEmail").innerHTML = "The email address contains illegal characters";
		return false;
	} else {
		field.style.background = 'White';
		document.getElementById("errorEmail").innerHTML = "";
		return true;
	}
}
function validatePeople(field){
	var illegalChars = /[a-zA-Z0]/;
	
	if(field.value == ""){
		field.style.background = 'Yellow';
		document.getElementById("errorPeople").innerHTML = "You didn't enter the amount of people";
		return false;
	} else if (illegalChars.test(field.value)) {
		document.getElementById("errorPeople").innerHTML = "The amount contains illegal characters";
		field.style.background = 'Yellow';
		return false;
	} else{
		field.style.background = 'White';
		document.getElementById("errorPeople").innerHTML = "";
		return true;
	}
}
function validateDate(field){
	var illegalChars = /[a-zA-Z]/;
	
	if(field.value == ""){
		field.style.background = 'Yellow';
		document.getElementById("errorDate").innerHTML = "You didn't enter the date";
		return false;
	} else if (illegalChars.test(field.value)) {
		document.getElementById("errorDate").innerHTML = "The date contains illegal characters";
		field.style.background = 'Yellow';
		return false;
	} else{
		field.style.background = 'White';
		document.getElementById("errorDate").innerHTML = "";
		return true;
	}
}function validateTime(field){
	var illegalChars = /[a-zA-Z]/;
	
	if(field.value == ""){
		field.style.background = 'Yellow';
		document.getElementById("errorTime").innerHTML = "You didn't enter the time";
		return false;
	} else if (illegalChars.test(field.value)) {
		document.getElementById("errorTime").innerHTML = "The time contains illegal characters";
		field.style.background = 'Yellow';
		return false;
	} else{
		field.style.background = 'White';
		document.getElementById("errorTime").innerHTML = "";
		return true;
	}
}