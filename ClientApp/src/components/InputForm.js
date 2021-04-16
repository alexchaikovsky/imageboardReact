import React from "react";

class InputForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = { text: "", file: null };

    this.handleTextChange = this.handleTextChange.bind(this);
    this.handleFileChange = this.handleFileChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleTextChange(event) {
    this.setState({ text: event.target.value });
    console.log(event.target.value);
  }

  handleFileChange(event) {
    this.setState({ file: event.target.files[0] });
    console.log("change file");
  }

  handleSubmit(event) {
    alert("A data was submitted: " + this.state.text);
    event.preventDefault();
    const uri = "api/posts/" + this.props.threadId;
    //const content = { Name: "", Subject: "", Text: this.state.value };
    let formData = new FormData();
    //formData.append('Text', this.this.state.value)
    formData.append("Text", this.state.text);
    formData.append("Images", this.state.file);

    fetch(uri, {
      method: "POST",
      //headers: {
      //  Accept: "application/json",
      //  "Content-Type": "application/json",
      //},
      body: formData,
    });
    this.props.parent.reload();
    this.props.parent.render();
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
                  text={this.state.text}
                  onChange={this.handleTextChange}
                />
              </td>
            </tr>
            <tr>
              <td class="postblock">File</td>
              <td>
                <input
                  type="file"
                  //value={this.state.file}
                  name="image"
                  size="35"
                  onChange={this.handleFileChange}
                />
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
