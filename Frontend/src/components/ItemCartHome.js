import React, { useState } from 'react';
import {
    AppBar,
    Toolbar,
    IconButton,
    Badge,
    Menu,
    MenuItem,
    Typography,
} from '@mui/material';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
import {useItemCart} from "../context/ItemCartProvider";
import ItemCart from "./ItemCart";

const ItemCartHome = () => {
    const [anchorEl, setAnchorEl] = useState(null);
    const {cartItems} = useItemCart();

    const handleClick = (event) => {
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };

    return (
        <div>
            <AppBar position="static">
                <Toolbar>
                    <IconButton color="inherit" onClick={handleClick}>
                        <Badge badgeContent={cartItems.length} color="error">
                            <ShoppingCartIcon/>
                        </Badge>
                    </IconButton>

                    <Menu
                        id="simple-menu"
                        anchorEl={anchorEl}
                        keepMounted
                        open={Boolean(anchorEl)}
                        onClose={handleClose}
                    >
                        <MenuItem onClick={handleClose}>
                            <Typography variant="h6">Twój koszyk</Typography>
                        </MenuItem>
                        <MenuItem onClick={handleClose}>
                            <ItemCart/>
                        </MenuItem>
                    </Menu>
                </Toolbar>
            </AppBar>
        </div>
    );
};

export default ItemCartHome;