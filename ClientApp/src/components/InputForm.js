import React from "react";

class InputForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = { value: "" };

    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleChange(event) {
    this.setState({ value: event.target.value });
    console.log("change");
  }

  handleSubmit(event) {
    alert("A name was submitted: " + this.state.value);
    event.preventDefault();
    const uri = "api/posts/1";
    const content = { Name: "", Subject: "", Text: this.state.value };
    fetch(uri, {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(content),
    });
  }
  render() {
    return (
      <form id="delform" onSubmit={this.handleSubmit}>
        <table>
          <tbody>
            <tr>
              <td class="postblock">Name</td>
              <td>
                <input type="text" name="field1" size="28" />
              </td>
            </tr>
            <tr>
              <td class="postblock">Subject</td>
              <td>
                <input type="text" name="field3" size="35" />
                <input type="submit" value="Submit" />
              </td>
            </tr>
            <tr>
              <td class="postblock">Comment</td>
              <td>
                <textarea
                  name="field"
                  cols="48"
                  rows="4"
                  value={this.state.value}
                  onChange={this.handleChange}
                />
              </td>
            </tr>
            <tr>
              <td class="postblock">File</td>
              <td>
                <input type="file" name="file" size="35" />
              </td>
            </tr>
            <tr>
              <td colspan="2">
                <div class="rules">
                  <ul>
                    <li>Supported file types are: GIF, JPG, PNG</li>
                    <li>Maximum file size allowed is 1000 KB.</li>
                    <li>
                      Images greater than 200x200 pixels will be thumbnailed.
                    </li>
                  </ul>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </form>
    );
  }
}
export default InputForm;
