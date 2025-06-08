import {jwtDecode} from "jwt-decode";
import axios from "axios";
import {ENDPOINTS, PATHS} from "../utils/consts";
import {useItemCart} from "../context/ItemCartProvider";
import {Box, Container, Paper, Typography} from "@mui/material";
import Button from "@mui/material/Button";
import {addDays, today} from "../utils/Date";
import {DateFormat} from "../utils/DateFormat";
import {useNavigate} from "react-router-dom";

const LoansPage = () => {
    const { cartItems, clearCart } = useItemCart();
    const token = localStorage.getItem('access_token');
    const start = today();
    const end = addDays(start, 14);
    const navigate = useNavigate();
    let userId;
    if(token)
    {
        const decodeToken = jwtDecode(token);
        userId = decodeToken.Id
    }
    const rentBooks = () => {
        const loanDto = {
            UserId: userId,
            BooksId: cartItems.map(item => ({bookId : item.id.toString(),quantity: item.quantity}))
        };

        const {data: newLoan} = axios.post(ENDPOINTS.addLoan,loanDto)
            .then(() => {
                clearCart();
                navigate(PATHS.loanConfirmSuccess,{state: {loan:newLoan}})
            })
            .catch(error => {
                console.log('Blad: ', error);
            })
    }
    return (
        <Container sx={{mt:3}}>
            <Typography variant="h2">Podsumowanie</Typography>
            <Paper sx={{p:3,my:3}}>
                {cartItems.map((item) => (
                    <Box sx={{m:2}} key={item.id}>
                        <Typography variant="h6" >Autor: {item.author} | Tytuł: {item.name} | Ilość: {item.quantity}</Typography>
                    </Box>
                ))}
                <Typography variant="h5">Start wypożyczenia: {DateFormat(start)}</Typography>
                <Typography variant="h5">Koniec wypożyczenia: {DateFormat(end)}</Typography>
                <Button variant='contained' sx={{m:2}} onClick={rentBooks} disabled={!cartItems.length}>Potwierdź wypożyczenie</Button>
            </Paper>
        </Container>
    )
}
export default LoansPage;