import React, { Component } from "react";
import { Route } from "react-router";
import { Layout } from "./components/Layout";
import { Home } from "./components/Home";
import { FetchData } from "./components/FetchData";
import { Board } from "./components/Board";
import ThreadWithRouter, { Thread } from "./components/Thread";
import { Counter } from "./components/Counter";

import "./custom.css";

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Route exact path="/" component={Home} />
        <Route path="/counter" component={Counter} />
        <Route path="/fetch-data" component={FetchData} />
        <Route path="/board" component={Board} />
        <Route path="/thread/:id" component={Thread} />
      </Layout>
    );
  }
}
