import React, {useState} from 'react';
import {Container, Typography, Grid, TextField, Button} from '@material-ui/core';
import style from '../Tool/Style';
import { registrarUsuario } from '../../actions/UsuarioAction';

const RegistrarUsuario = () => {
        //variable de estado para registrar en el backend
        const [usuario, setUsuario] = useState({
            //inicializamos todos los campos vacios
            //los nombres deben coincidir con los nombre de las cajas de texto
            NombreCompleto : '',
            Email : '',
            Password : '',
            ConfirmarPassword: '',
            Username: ''

        })

        //esta funcion recibe los valores de la caja de texto con el metodo onChange y el value
        const ingresarValoresMemoria = e =>{
            const {name, value} = e.target;
            //que mantenga los valores que tenia y solo los cambie por los valores que se ingresen
            setUsuario( anterior => ({
                ...anterior,
                //NombreCompleto : 'Facundo Marcos'
                [name] : value
            }))
        }

        const registrarUsuarioBoton = e => {
            //revisar, no toma prevent default
            e.preventDefault();
            //e.preventDefault();
            registrarUsuario(usuario).then(response => {
                console.log('se registro exitosamente el usuario', response);
                window.localStorage.setItem("token_seguridad", response.data.token);
            });
        }

    return(
        //md para visualizar tamaño maximo en computadora
        //xs smartphone
        //sm tablet
        <Container component="main" maxWidth="md" justify="center">

            <div style={style.paper}>
               
                <Typography component="h1" variant="h5">
                    Registro de Usuario
                </Typography>
                <form style={style.form}>
                    <Grid container spacing={2}>
                        <Grid item xs={12} md={12}>
                            <TextField name="NombreCompleto" value={usuario.NombreCompleto} onChange={ingresarValoresMemoria} variant="outlined" fullWidth label="Ingrese su Nombre y Apellidos"/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="Email" value={usuario.Email} onChange={ingresarValoresMemoria} variant="outlined" fullWidth label="Ingrese su email"/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="Username" value={usuario.Username} onChange={ingresarValoresMemoria} variant="outlined" fullWidth label="Ingrese su Username"/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="Password" value={usuario.Password} onChange={ingresarValoresMemoria} type="password" variant="outlined" fullWidth label="Ingrese Password"/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="ConfirmarPassword" value={usuario.ConfirmarPassword} onChange={ingresarValoresMemoria} type="password" variant="outlined" fullWidth label="Confirme Password"/>
                        </Grid>

                    </Grid>
                    <Grid container justify="center">
                        <Grid item xs={12} md={6}>
                            <Button type="submit" onClick={registrarUsuarioBoton} fullWidth variant="contained" color="primary" size="large" style={style.submit}>
                                Enviar
                            </Button>
                        </Grid>

                    </Grid>
                </form>
            </div>
        </Container>
    );

}

export default RegistrarUsuario;
