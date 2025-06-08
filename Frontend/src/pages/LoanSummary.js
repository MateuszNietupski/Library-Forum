import {Link, useParams} from "react-router-dom";
import {Box, Container, Typography} from "@mui/material";
import {PATHS} from "../utils/consts";
import Button from "@mui/material/Button";
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import {useState} from "react";

const LoanSummary = () => {
      const {state} = useParams();
      const [loan, setLoan] = useState(state?.loan || null);
      
      return (
          <Container>
              <Box textAlign="center" mb={4}>
                  <CheckCircleIcon color="success" sx={{ fontSize: 60 }} />
                  <Typography variant="h3" gutterBottom>Dziękujemy!</Typography>
                  <Typography variant="h6">
                      Twoje wypożyczenie o nr <b>{loan.id}</b> zostało utworzone.Zapraszamy po odbiór książek.
                  </Typography>
              </Box>
              <Box textAlign="center">
                  <Button
                      component={Link}
                      variant="contained"
                      sx={{ mr: 2 }}
                  >
                      Moje wypożyczenia
                  </Button>
                  <Button
                      component={Link}
                      to={PATHS.books}
                      variant="outlined"
                  >
                      Przeglądaj książki
                  </Button>
              </Box>
          </Container>
      )
}
export default LoanSummary;