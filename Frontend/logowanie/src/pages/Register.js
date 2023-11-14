import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { useState } from 'react';
import { ENDPOINTS } from '../utils/consts';
import axios from 'axios';
import { Link } from 'react-router-dom';
import { PATHS } from '../utils/consts';
import { useNavigate } from 'react-router-dom';



export default function Register() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [name, setName] = useState('');
  const [errors, setErrors] = useState([]);
  const navigate = useNavigate();
  const handleSubmit = async (event) => {
    event.preventDefault();
      axios.post(ENDPOINTS.register, {
        email,
        password,
        name
      }).then(response =>{
        if (response.data.result) {
          navigate(PATHS.login);
        } else {
          setErrors(response.data.errors);
          alert(response.data.errors);
        }
      })
      .catch(error => {
        
        console.error('There was an error!', error);
      });
  };

  return (
      <Container component="main" maxWidth="xs">
        <CssBaseline />
        <Box
          sx={{
            marginTop: 8,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
          }}
        >
          <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component="h1" variant="h5">
            Rejestracja
          </Typography>
          <Box component="form" noValidate onSubmit={handleSubmit} sx={{ mt: 3 }}>
            <Grid container spacing={2}>
              <Grid item xs={12}>
                <TextField
                  autoComplete="given-name"
                  name="name"
                  required
                  fullWidth
                  id="name"
                  label="name"
                  value={name} 
                  onChange={(event) => setName(event.target.value)}
                  autoFocus
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  id="email"
                  label="Email Address"
                  name="email"
                  value={email}
                  onChange={(event) => setEmail(event.target.value)}
                  autoComplete="email"
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  name="password"
                  label="Password"
                  type="password"
                  id="password"
                  value={password}
                  onChange={(event) => setPassword(event.target.value)}
                  autoComplete="new-password"
                />
              </Grid>
            </Grid>
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
            >
              Utwórz konto
            </Button>

            <Grid container justifyContent="flex-end">
              <Grid item>
                <Link href={PATHS.login} variant="body2">
                  Masz konto? Zaloguj się.
                </Link>
              </Grid>
            </Grid>
            {errors.length > 0 && (
              <div>
                <h2>Błąd:</h2>
                <ul>
                  {errors.map((error, index) => (
                    <li key={index}>{error}</li>
                  ))}
                </ul>
              </div>
            )}
          </Box>
        </Box>
      </Container>
  );
}