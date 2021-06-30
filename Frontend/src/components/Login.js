import React, { useState } from "react";

function Login(props) {
  const username = useFormInput("");
  const password = useFormInput("");
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(false);

  // handle button click of login form
  const handleLogin = async () => {
    //props.history.push("/dashboard");
    let formData = new FormData();
    //formData.append('Text', this.this.state.value)
    formData.append("username", username.value);
    formData.append("password", password.value);
    const response = await fetch("token", {
      method: "POST",
      //headers: {
      //  Accept: "application/json",
      //  "Content-Type": "application/json",
      //},
      body: formData,
    });
    const data = await response.json();
    console.log(data);
  };

  return (
    <div>
      Login
      <br />
      <br />
      <div>
        Username
        <br />
        <input type="text" {...username} autoComplete="new-password" />
      </div>
      <div style={{ marginTop: 10 }}>
        Password
        <br />
        <input type="password" {...password} autoComplete="new-password" />
      </div>
      {error && (
        <>
          <small style={{ color: "red" }}>{error}</small>
          <br />
        </>
      )}
      <br />
      <input
        type="button"
        className="btn btn-primary"
        value={loading ? "Loading..." : "Login"}
        onClick={handleLogin}
        disabled={loading}
      />
      <br />
    </div>
  );
}

const useFormInput = (initialValue) => {
  const [value, setValue] = useState(initialValue);

  const handleChange = (e) => {
    console.log(e.target.value);
    setValue(e.target.value);
  };
  return {
    value,
    onChange: handleChange,
  };
};

export default Login;
