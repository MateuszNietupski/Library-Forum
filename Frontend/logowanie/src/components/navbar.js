import { useContext } from "react";
import { AuthContext } from "../context/AuthProvider";
import { PATHS } from "../utils/consts";
import { Container, Nav, Navbar } from "react-bootstrap";
import AddNews from "../pages/AddNews";
import { Link } from "react-router-dom";

const Header = () => {
    //<Navbar.Brand as={Link} to={PATHS.addNews}>Dodaj Wpis</Navbar.Brand>
    const {isLoggedIn, logout} = useContext(AuthContext);
    return (
        <Navbar collapseOnSelect expand="lg" bg="dark" variant="dark">
            <Container>
                <Navbar.Brand as={Link} to={PATHS.home}>Home</Navbar.Brand>
                
                <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                    <Navbar.Collapse id="responsive-navbar-nav">
                        <Nav className="me-auto">
                            {isLoggedIn && (
                            <>
                                <Nav.Link onClick={logout}>Logout</Nav.Link>
                            </>
                            )}
                        </Nav>
                    </Navbar.Collapse>
            </Container>
        </Navbar>
    );
};

export default Header;