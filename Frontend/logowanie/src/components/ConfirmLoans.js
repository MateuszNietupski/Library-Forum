import React, {useEffect, useState} from 'react';
import { Card, CardContent, Typography, List, ListItem, ListItemText } from '@mui/material';
import axios from "axios";
import {ENDPOINTS} from "../utils/consts";
import axiosAuth from "../utils/authInstance";
import Button from "@mui/material/Button";

const LoanList = ({}) => {
    const [data, setData] = useState([]);

    useEffect(() => {
        
        axios.get(ENDPOINTS.getLoans)
            .then(response => 
            setData(response.data))
            .catch(err => 
            console.log(err))
        
    }, []);

    const handleApprove = async (loanId) => {
        
        try {
           axiosAuth.patch(ENDPOINTS.loanConfirm, { id: loanId })
               .then(console.log("ok"))
               .catch(err => console.log(err))
            console.log(`Loan ID ${loanId} został zatwierdzony`);
        } catch (error) {
            console.error('Błąd podczas zatwierdzania:', error);
        }
    };
    
    return (
        <div>
            {data.map((item) => (
                <Card key={item.loan.id} style={{ margin: '10px' }}>
                    <CardContent>
                        <Typography variant="h6">Loan ID: {item.loan.id}</Typography>
                        <List>
                            {item.books.map((book) => (
                                <ListItem key={book.id}>
                                    <ListItemText>
                                        <Typography variant="h6" component="div">
                                            Tytul: {book.name}
                                        </Typography>
                                        <Typography variant="body2" color="text.secondary">
                                            Autor: {book.author}
                                        </Typography>
                                    </ListItemText>
                                </ListItem>
                            ))}
                        </List>
                        <Button onClick={() => handleApprove(item.loan.id)}  >
                            Zatwierdź
                        </Button>
                    </CardContent>
                </Card>
            ))}
        </div>
    );
};

export default LoanList;