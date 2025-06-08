import {useItemCart} from "../context/ItemCartProvider";
import Button from "@mui/material/Button";
import {PATHS} from "../utils/consts";
import {Link} from "react-router-dom";

const ItemCart = () => {
    const { cartItems, removeItemFromCart, clearCart } = useItemCart();
    
    return (
        <div>
            <ul>
                {cartItems.map((item) => (
                    <li key={item.id}>
                        {item.name}
                        {" Ilość: "} {item.quantity}
                        <Button onClick={() => removeItemFromCart(item.id)}>Remove</Button>
                    </li>
                ))}
            </ul>
            <Button onClick={clearCart}>Wyczyść</Button>
            <Button as={Link} to={PATHS.loanConfirm}>Wypożycz</Button>
        </div>
    );
};

export default ItemCart;