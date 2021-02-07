import React, { useState } from 'react';
import style from '../Tool/Style';
import { Avatar, Button, Container, Typography, TextField } from '@material-ui/core';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import { loginUsuario } from '../../actions/UsuarioAction';

const Login = () => {

    //variable de estado
    const [usuario, setUsuario] = useState({
        Email : '',
        Password : ''
    })

    const ingresarValoresMemoria = e => {
        const {name, value} = e.target;
        setUsuario(anterior => ({
            ...anterior,
            [name]: value
        }))
    }


   const loginUsuarioBoton = e => {
       e.preventDefault();
       loginUsuario(usuario).then(response => {
           console.log('login exitoso', response);
           window.localStorage.setItem('token_seguridad', response.data.token);
       })
   }

    return (
        
        <Container maxWidth = "xs">
            <div style={style.paper}>
                <Avatar style={style.avatar}>
                    <LockOutlinedIcon style={style.icon}/>
                </Avatar>
                <Typography component="h1" variant="h5">
                    Login de Usuario
                </Typography>
                <form style={style.form}>
                    <TextField name="Email" value={usuario.Email} onChange={ingresarValoresMemoria} variant="outlined" label="Ingrese Username"  fullWidth margin="normal" />
                    <TextField name="Password" value={usuario.Password} onChange={ingresarValoresMemoria} variant="outlined" type="password" label="Password" fullWidth margin="normal" />
                    <Button  type="submit" onClick={loginUsuarioBoton} fullWidth variant="contained" color="primary" style={style.submit}>
                        Enviar
                    </Button>
                </form>
            </div>
        </Container>
    );
};

export default Login;