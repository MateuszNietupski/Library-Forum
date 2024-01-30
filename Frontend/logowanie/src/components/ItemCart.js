import {useItemCart} from "../context/IterCartProvider";
import Button from "@mui/material/Button";
import axios from "axios";
import {ENDPOINTS} from "../utils/consts";
import {jwtDecode} from "jwt-decode";

const ItemCart = () => {
    const { cartItems, removeItemFromCart, clearCart } = useItemCart();
    const token = localStorage.getItem('access_token');
    let userId;
    if(token)
    {
        const decodeToken = jwtDecode(token);
        userId = decodeToken.Id
    }
    const rentBooks = () => {
        
        const loanDto = {
            UserId: userId, 
            BooksId: cartItems.map(item => item.id.toString())
        };
        
        axios.post(ENDPOINTS.addLoan,loanDto)
            .then(() => {
                clearCart();
            })
            .catch(error => {
                console.log('Blad: ', error);
            })
    }

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
            <Button onClick={rentBooks}>Zatwierdź</Button>
        </div>
    );
};

export default ItemCart;