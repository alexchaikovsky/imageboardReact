import React, { Component } from "react";
import Post from "./Post";
import MainPost from "./MainPost";
import "./futaba.css";
import "./main.css";
import InputForm from "./InputForm";
import { BallBeat } from "react-pure-loaders";
import {boardUrlDev} from "../config/configuration.js";

export class Thread extends Component {
  static displayName = Thread.name;

  constructor(props) {
    super(props);
    this.state = { opPost: {}, posts: [], loading: true };
    this.formSend = this.formSend.bind(this);
    //this.id = useParams();
  }

  componentDidMount() {
    this.populatePostsData();
  }

  static renderPostsTable(opPost, posts) {
    //console.log("mainpost");
    //console.log(opPost);
    //console.log(posts);
    return (
      <div>
        {<MainPost data={opPost} />}
        {posts.map((post) => (
          <Post key={post.id} data={post} />
        ))}
      </div>
    );
  }

  render() {
    let contents = this.state.loading ? (
      <div className="postarea">
        <BallBeat color={"#123abc"} loading={this.state.loading} />
      </div>
    ) : (
      //<p>
      //  <em>Loading...</em>
      //</p>
      Thread.renderPostsTable(this.state.opPost, this.state.posts)
    );

    return (
      <div>
        <h1 id="tabelLabel">Thread</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {
          <InputForm
            threadId={this.props.match.params.id}
            onFormSend={this.formSend}
          />
        }
        {contents}
        <br />
        <button
          className="btn btn-outline-primary btn-block"
          //onClick={this.componentDidMount.bind(this)}
          onClick={this.reload.bind(this)}
        >
          Update
        </button>
      </div>
    );
  }
  reload() {
    //this.componentDidMount();
    this.populatePostsData();
    //alert("gav gav");
    //this.setState({ loading: true });
  }
  async populatePostsData() {
    console.log("fetch + " + boardUrlDev);
    let id = this.props.match.params.id;
    const response = await fetch(boardUrlDev + "api/posts/" + id);
    if (response.status == 200) {
      const data = await response.json();
      const first = data.shift();
      this.setState({ opPost: first, posts: data, loading: false });
    } else {
    }
  }
  formSend(event) {
    this.reload();
  }
}
