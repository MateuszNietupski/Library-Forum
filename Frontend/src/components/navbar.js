import { useContext } from "react";
import { AuthContext } from "../context/AuthProvider";
import { PATHS } from "../utils/consts";
import { Container, Nav, Navbar } from "react-bootstrap";
import { Link } from "react-router-dom";
import SunIcon from "../icons/SunIcon";
import SwitchTheme from "./SharedLayout/SwitchTheme";
import MoonIcon from "../icons/MoonIcon";
import ItemCartHome from "./ItemCartHome";


const Header = () => {
    const {isLoggedIn, logout} = useContext(AuthContext);
    //<Navbar.Brand as={Link} to={PATHS.forum}>Forum</Navbar.Brand>
    
    return (
        <Navbar collapseOnSelect expand="lg" bg="dark" variant="dark">
            <Container>
                
                <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                    <Navbar.Collapse id="responsive-navbar-nav">
                        <SunIcon/>
                        <SwitchTheme/>
                        <MoonIcon/>
                        <Navbar.Brand as={Link} to={PATHS.home}>Home</Navbar.Brand>
                        <Navbar.Brand as={Link} to={PATHS.adminPanel}>AdminPanel</Navbar.Brand>
                        <Navbar.Brand as={Link} to={PATHS.books}>Books</Navbar.Brand>
                    </Navbar.Collapse>
                <ItemCartHome/>
                <Nav className="me-auto">
                    {isLoggedIn && (
                        <>
                            <Nav.Link onClick={logout}>Logout</Nav.Link>
                        </>
                    )}
                </Nav>
            </Container>
        </Navbar>
    );
};

export default Header;