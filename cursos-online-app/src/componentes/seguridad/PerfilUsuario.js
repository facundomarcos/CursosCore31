import React, {useState} from 'react';
import { Container,Typography, Grid, TextField, Button } from '@material-ui/core';
import style from '../Tool/Style';


const PerfilUsuario = () => {
    //variable de estado para manejar el token
    const [usuario, setUsuario] = useState({
            NombreCompleto : '',
            Email : '',
            Password : '',
            ConfirmarPassword: ''
    })

    //esta funcion recibe los valores de la caja de texto con el metodo onChange y el value
    const ingresarValoresMemoria = e => {
        const {name, value} = e.target;
        //que mantenga los valores que tenia y solo los cambie por los valores que se ingresen
        setUsuario( anterior => ({
            ...anterior,
            [name] : value
        }));
    }



    return (
        <Container component="main" maxWidth="md" justify="center">
            <div style={style.paper}>
                <Typography component="h1" variant="h5">
                    Perfil de Usuario
                </Typography>
            </div>
            <form style={style.form}>
                <Grid container spacing={2}>
                    <Grid item xs={12} md={12}>
                        <TextField name="NombreCompleto" value={usuario.NombreCompleto} onChange={ingresarValoresMemoria} variant="outlined" fullWidth label="Ingrese Nombre y Apellidos"/>
                    </Grid>
                    <Grid item xs={12} md={6}>
                        <TextField name="Email" value={usuario.Email} onChange={ingresarValoresMemoria} variant="outlined" fullWidth label="Ingrese email"/>
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
                        <Button type="submit" fullWidth variant="contained" size="large" color="primary" style={style.submit}>
                            Guardar Datos
                        </Button>
                    </Grid>
                </Grid>
            </form>
        </Container>
    );
};

export default PerfilUsuario;