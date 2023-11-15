import "./Login.css";
import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.css";

import { notifyMessage, postAPICall } from "../../api/api";

export default function Login() {
  const [Pusername, setUsername] = useState("");
  const [Ppassword, setPassword] = useState("");
  const [data, SetLoginresponseData] = useState([]);
  let navigate = useNavigate();
  //username correct validation
  const [validation, setValidation] = useState("");
  const [isValid, setIsValid] = useState(false);
  //password empty
  const [RequiredValidation, setRequiredValidation] = useState("");
  const [error, setError] = useState("");
  //remember me
  const [rememberMe, setRememberMe] = useState(false);
  const [passwordVisible, setPasswordVisible] = useState(false);

  //  const history =useHistory ();
  const updateUsername = (e) => {
   // validation  textbox
    const inputValue = e.target.value;
    const regexPattern = /^[a-zA-Z0-9]+$/;
    const isValidInput = regexPattern.test(inputValue);
    setValidation(inputValue);
    //setIsValid(isValidInput);
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
    debugger;
    setRememberMe(e.target.checked);
  };
  const togglePasswordVisibility = () => {
    setPasswordVisible(!passwordVisible);
  };

  const requestBody = {
    Pusername: Pusername,
    Ppassword: Ppassword,
  };

  const handlePost = async () => {
    try {
      localStorage.clear();
      const response = await postAPICall("api/PortalLogin/login", requestBody); // Replace 'posts' with your API endpoint
      console.log("POST Response:", response);
      if (response.data) {
        localStorage.setItem("logginuser", Pusername); //JSON.stringify(Pusername));
        localStorage.setItem("Loginusertype", response.data[0].UserType);
        let Type = response.data[0].UserType;
        // if (Pusername && Ppassword && Type)
        //   notifyMessage("Login successfully", "success");
        navigate("/schedule");
      }
    } catch (error) {
      console.error("POST Error:", notifyMessage("Login Failed", "warning"));
    }
  };
  //Remember Me
  const handleSubmit = (e) => {
    debugger;
    e.preventDefault();
   
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
            {!isValid ? (
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
