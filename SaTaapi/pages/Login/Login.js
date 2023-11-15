import "./Login.css";
import React, { useState } from "react";
//import { useNavigate } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.css";
import { useNavigate } from "react-router-dom";
import { notifyMessage, postAPICall } from "../../api/api";

export default function Login() {
  const [Pusername, setUsername] = useState("");
  const [Ppassword, setPassword] = useState("");
  //username correct validation
  const [validation, setValidation] = useState("");
  const [isValid, setIsValid] = useState(true);

  //password empty
  const [RequiredValidation, setRequiredValidation] = useState("");
  const [error, setError] = useState("");
  //remember me
  const [rememberMe, setRememberMe] = useState(false);
  let navigate = useNavigate();
  //let navigate = useNavigate();
  //  const history =useHistory ();
  const updateUsername = (e) => {
    //validation  textbox
    const inputValue = e.target.value;
    const regexPattern = /^[a-zA-Z0-9]+$/;
    const isValidInput = regexPattern.test(inputValue);
    setValidation(inputValue);
    setIsValid(isValidInput);
    //check username is correct ApI
    setUsername(e.target.value);
  };

  const updatepassword = (e) => {
    setPassword(e.target.value);
    const inputValue = e.target.value;
    //check textbox  empty
    setRequiredValidation(inputValue);

    if (inputValue.trim() === "") {
      setError("Password is required.");
    } else {
      setError("");
    }
  };
  //Remember ME
  const handleRememberMeChange = (e) => {
    setRememberMe(e.target.checked);
  };
  //show password Icon Hide/visible
  const [passwordVisible, setPasswordVisible] = useState(false);

  const togglePasswordVisibility = () => {
    setPasswordVisible(!passwordVisible);
  };
  const requestBody = {
    userName: Pusername,
    password: Ppassword,
  };
  const handlePost = async () => {
    try {debugger
      const response = await postAPICall("api/Users/SPUserLogin", requestBody); // Replace 'posts' with your API endpoint
      console.log("POST Response:", response);
      if (response.data.length > 0) {
        localStorage.setItem("activeUser",response.data[0].userName) 
        localStorage.setItem("Getuserid",response.data[0].userId)
         localStorage.setItem("Loginusertype", response.data[0].userType);
        navigate("/channelPopup");
      } else {
        notifyMessage("Login Failed", "warning");
      }
    } catch (error) {
      console.error("POST Error:", error);
      notifyMessage("Login Failed", "warning");
    }
  };
  //Remember Me
  const handleSubmit = (e) => {
    e.preventDefault();
    // Here, you can handle the form submission, for example, by sending the data to an API.
    console.log("User:", Pusername);
    console.log("Password:", Ppassword);
    // console.log('Remember Me:', rememberMe);
    // Reset the form fields if needed
    setUsername("");
    setPassword("");
    setRememberMe(false);
  };
  return (
    <div className="container1">
      <form onSubmit={handleSubmit}>
        <div class="wrapper">
          <h1>Login</h1>
          <div class="input-container">
            <i class="bx bxs-user text-dark"></i>
            <input
              type="text"
              placeholder="Username"
              required
              id="txtbox"
              onChange={updateUsername}
              value={validation}
            />
            {isValid ? (
              <p></p>
            ) : (
              <p className="text-danger">
                It should contain albhabetic characters and Numbers.
              </p>
            )}
          </div>
          <div class="input-container">
            <input
              type={passwordVisible ? "text" : "password"}
              placeholder="Password"
              id="txtbox"
              onChange={updatepassword}
              required
              value={RequiredValidation}
            />
            <i
              className={`toggle-password text-dark fa ${
                passwordVisible ? "fa-eye" : "fa-eye-slash"
              } eye-icon}`}
              onMouseDown={togglePasswordVisibility}
              onMouseUp={togglePasswordVisibility}
            >
              {passwordVisible}
            </i>
            {error && <p style={{ color: "red" }}>{error}</p>}
            <i class="bx bxs-lock-alt text-dark"></i>
          </div>
          <div class="remember-forgot">
            <label>
              <input
                type="checkbox"
                checked={rememberMe}
                onChange={handleRememberMeChange}
              />
              Remember Me
            </label>
            <a href="#">Forgot Password</a>
          </div>
          <br />
          <button type="submit" class="btn" onClick={handlePost}>
            Login
          </button>
        </div>
      </form>
    </div>
  );
}
