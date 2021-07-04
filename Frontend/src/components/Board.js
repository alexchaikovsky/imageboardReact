import React, { Component } from "react";
import Post from "./Post";
import MainPost from "./MainPost";
import "./futaba.css";
import "./main.css";
import InputForm from "./InputForm";
import {boardUrlDev} from "../config/configuration.js";

export class Board extends Component {
  static displayName = Board.name;

  constructor(props) {
    super(props);
    this.state = { posts: [], loading: true };
  }

  componentDidMount() {
    this.populatePostsData();
  }

  static renderPostsTable(posts) {
    return (
      <div>
        {posts.map((post) => (
          <MainPost key={post.id} data={post} />
        ))}
      </div>
    );
  }

  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      Board.renderPostsTable(this.state.posts)
    );
    //{<InputForm parentReload={this.populatePostsData()} />}
    return (
      <div>
        <h1 id="tabelLabel">Thread</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {<InputForm />}
        {contents}
      </div>
    );
  }

  async populatePostsData() {
    const response = await fetch(boardUrlDev + "api/posts").catch((error) => {console.error('Error:', error); });
    console.log(response);
    const data = await response.json();
    this.setState({ posts: data, loading: false });
  }
}
