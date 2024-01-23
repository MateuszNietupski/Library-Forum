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
    
    
    return (
        <Navbar collapseOnSelect expand="lg" bg="dark" variant="dark">
            <Container>
                
                <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                    <Navbar.Collapse id="responsive-navbar-nav">
                        <SunIcon/>
                        <SwitchTheme/>
                        <MoonIcon/>
                        <Navbar.Brand as={Link} to={PATHS.home}>Home</Navbar.Brand>
                        <Navbar.Brand as={Link} to={PATHS.forum}>Forum</Navbar.Brand>
                        <Navbar.Brand as={Link} to={PATHS.adminPanel}>AdminPanel</Navbar.Brand>
                        <Navbar.Brand as={Link} to={PATHS.books}>Books</Navbar.Brand>
                        <Nav className="me-auto">
                            {isLoggedIn && (
                                <>
                                    <Nav.Link onClick={logout}>Logout</Nav.Link>
                                </>
                            )}
                        </Nav>
                    </Navbar.Collapse>
                <ItemCartHome/>
                
            </Container>
        </Navbar>
    );
};

export default Header;