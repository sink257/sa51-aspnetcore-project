window.onload = function () {
	let errDiv = document.getElementById("loginerror");

	let form = document.getElementById("form");
	form.onsubmit = function () {
		let elemUname = document.getElementById("username");
		let elemPwd = document.getElementById("password");

		let username = elemUname.value.trim();
		let password = elemPwd.value.trim();

		if (username.length === 0 || password.length === 0) {
			errDiv.innerHTML = "Please fill up all fields.";
			return false;	// cancel form submission
		}

		return true;	// allow form submission to continue
	}

	let elems = document.getElementsByClassName("form-control");
	for (let i = 0; i < elems.length; i++) {
		// remove our error message as long as any 
		// of the input boxes have focus
		elems[i].onfocus = function () {
			errDiv.innerHTML = "";
		}
	}
}
