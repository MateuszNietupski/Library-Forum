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
        
        const request = {
          UserId: userId,
          Books: cartItems.map(item => ({
              id: item.id,
              name: item.name,
              author: item.author,
              bookQuantity: item.quantity
          }))  
        };
        
        axios.post(ENDPOINTS.confirmationMail,request)
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