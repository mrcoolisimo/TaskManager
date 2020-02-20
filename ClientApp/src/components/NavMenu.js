import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render () {
      return (
        <header>
            <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-blue border-bottom box-shadow mb-3">
                <div class="container">
                      <a class="navbar-brand" href=""><span class="blue2">Mr</span><span class="yellow">Coolisimo</span></a>
                    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <div class="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse">
                        <partial name="_LoginPartial" />
                        <ul class="navbar-nav flex-grow-1">
                            <li class="nav-item">
                                <a class="text-dark nav-link" href="Projects">Projects</a>
                            </li>
                            <li class="nav-item">
                                <a class="text-dark nav-link" href="Blogs">Blogs</a>
                            </li>
                            <li class="nav-item">
                                <a class="text-dark nav-link" href="App">MyMacroPal</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        </header>
    );
  }
}
