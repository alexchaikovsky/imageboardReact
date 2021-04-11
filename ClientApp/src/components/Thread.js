import React, { Component } from "react";
import Post from "./Post";
import MainPost from "./MainPost";
import "./futaba.css";
import "./main.css";
import InputForm from "./InputForm";
import { useParams, withRouter } from "react-router";

export class Thread extends Component {
  static displayName = Thread.name;

  constructor(props) {
    super(props);
    this.state = { posts: [], loading: true };
    //this.id = useParams();
  }

  componentDidMount() {
    this.populatePostsData();
  }

  static renderPostsTable(posts) {
    return (
      <div>
        {<MainPost data={posts[0]} />}
        {posts.map((post) => (
          <Post key={post.id} data={post} />
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
      Thread.renderPostsTable(this.state.posts)
    );

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
    //let id = useParams();
    let id = this.props.match.params.id;
    const response = await fetch("api/posts/" + id);
    const data = await response.json();
    this.setState({ posts: data, loading: false });
  }
}
