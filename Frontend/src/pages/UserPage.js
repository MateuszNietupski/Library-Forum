import { useEffect, useState, useCallback } from 'react';
import axios from 'axios';
import {jwtDecode} from 'jwt-decode';
import {
    Typography,
    CircularProgress,
    Button,
    Stack,
    Alert, Container,
} from '@mui/material';
import LoansSection from "../components/LoansSection";
import {ENDPOINTS} from "../utils/consts";

const UserPage = () => {
    const [loans,    setLoans]    = useState([]);
    const [loading,  setLoading]  = useState(false);
    const [error,    setError]    = useState(null);
    const { id: userId } =
        jwtDecode(localStorage.getItem('access_token') || '{}');
    const fetchLoans = useCallback(async () => {
        try {
            setLoading(true);
            setError(null);

            const response = await axios.get(ENDPOINTS.getUserLoans(userId));
            const data = response.data.sort((a, b) => {
                const aActive = !a.ReturnDate;
                const bActive = !b.ReturnDate;
                if (aActive !== bActive) return aActive ? -1 : 1;
                return new Date(b.start) - new Date(a.start);
            });

            setLoans(data);
        } catch (err) {
            setError('Nie udało się pobrać wypożyczeń.');
            console.error(err);
        } finally {
            setLoading(false);
        }
    }, [userId]);
    
    useEffect(() => { fetchLoans(); }, [fetchLoans]);

    if (loading) return <CircularProgress sx={{ mt: 4 }} />;
    if (error)   return <Alert severity="error" sx={{ mt: 4 }}>{error}</Alert>;
    
    const active   = loans.filter(l => !l.ReturnDate);
    const finished = loans.filter(l =>  l.ReturnDate);

    return (
        <Container sx={{mt:2}}>
            <Stack direction="row" justifyContent="space-between" sx={{ mb: 2 }}>
                <Typography variant="h5">Wypożyczenia</Typography>
                <Button size="small" onClick={fetchLoans}>Odśwież</Button>
            </Stack>
            <LoansSection title="Aktywne"  loans={active}   chipColor="success" />
            <LoansSection title="Historia" loans={finished} chipColor="default" />
        </Container>
    );
}
 export default UserPage;