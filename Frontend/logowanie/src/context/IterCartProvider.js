import { createContext, useContext, useState } from 'react';

const ItemCartContext = createContext();

export const ItemCartProvider = ({children}) => {
    const [cartItems, setCartItems] = useState([]);

    const addItemToCart = (item) => {
        const existingItemIndex = cartItems.findIndex((cartItem) => cartItem.id === item.id);

        if (existingItemIndex !== -1) {
            
            const updatedCartItems = [...cartItems];
            updatedCartItems[existingItemIndex].quantity += 1;
            setCartItems(updatedCartItems);
        } else {
            
            setCartItems([...cartItems, { ...item, quantity: 1 }]);
        }
    };
    const removeItemFromCart = (itemId) => {
        const updatedCartItems = cartItems.map((item) =>
            item.id === itemId ? { ...item, quantity: Math.max(0, item.quantity - 1) } : item
        );
        setCartItems(updatedCartItems.filter((item) => item.quantity > 0));
    };

    const clearCart = () => {
        setCartItems([]);
    };

    return (
        <ItemCartContext.Provider
            value={{ cartItems, addItemToCart, removeItemFromCart, clearCart }}
        >
            {children}
        </ItemCartContext.Provider>
    );
};

export const useItemCart = () => {
    return useContext(ItemCartContext);
};