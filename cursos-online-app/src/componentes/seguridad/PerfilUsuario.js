import React, {useState, useEffect} from 'react';
import { Container,Typography, Grid, TextField, Button, Avatar } from '@material-ui/core';
import style from '../Tool/Style';
import { actualizarUsuario, obtenerUsuarioActual } from '../../actions/UsuarioAction';
import { obtenerDataImagen } from '../../actions/ImagenAction';
import { useStateValue } from '../../contexto/store';
import reactFoto from '../../logo.svg';
import {v4 as uuidv4} from 'uuid';
import ImageUploader from 'react-images-upload';

const PerfilUsuario = () => {
    const [{sesionUsuario }, dispatch] = useStateValue();
    //variable de estado para manejar el token
    const [usuario, setUsuario] = useState({
            nombreCompleto : '',
            email : '',
            password : '',
            confirmarPassword: '',
            username : '',
            //foto tienen que ser un arreglo para poder guardarlo en el servidor
            imagenPerfil : null,
            fotoUrl : ''
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

    //useEffect
    //que se rellenen los datos cuando haya terminado la carga
    //se ejecuta una sola vez xq no se le esta pidiendo que evalue ninguna variable
    useEffect(( )=> {
        obtenerUsuarioActual().then(response => {
            console.log('data del response', response);
            //rellene los datos con el response
            setUsuario(response.data);
        });
    }, [])

    //guardar usuario
    //se genera un nuevo token y hay que guardarlo en el localstorage
    const guardarUsuario = e => {
        //evita que refresque toda la pagina, solo refresca lo que se modifica
        e.preventDefault();
        actualizarUsuario(usuario).then(response => {

                if(response.status === 200){
                    dispatch({
                        type : "OPEN_SNACKBAR",
                        openMensaje : {
                            open : true,
                            mensaje : "Se guardaron exitosamente los cambios en Perfil Usuario"
                        }
                    })
                    window.localStorage.setItem("token_seguridad", response.data.token);
                }else{
                    dispatch({
                        type : "OPEN_SNACKBAR",
                        openMensaje : {
                            open : true,
                            mensaje : "Errores al intentar guardar en: " + Object.keys(response.data.errors) 
                        }
                    })
                }

            //console.log('se actualizó el usuario', response);
           // 
        })
    }

    //capturar la foto
    const subirFoto = imagenes => {
        const foto = imagenes[0];
        //el archivo lo convierte en una url local
        const fotoUrl = URL.createObjectURL(foto);

        //llama funcion global para obtener la extension del archivo
        //src/actions/ImagenAction.js
        obtenerDataImagen(foto).then(respuesta => {
            console.log('respuesta', respuesta);
            setUsuario(anterior => ({
                ...anterior,
                imagenPerfil : respuesta, //respuesta es un json que proviene del action obtener imagen {data: ..., nombre: ...etc}
                fotoUrl : fotoUrl //archivo en formato URL
            }) );
        })

        
    }

    const fotoKey = uuidv4();

    return (
        <Container component="main" maxWidth="md" justify="center">
            <div style={style.paper}>
                <Avatar style={style.avatar} src={usuario.fotoUrl || reactFoto} />
                <Typography component="h1" variant="h5">
                    Perfil de Usuario
                </Typography>
           
            <form style={style.form}>
                <Grid container spacing={2}>
                    <Grid item xs={12} md={12}>
                        <TextField name="nombreCompleto" value={usuario.nombreCompleto || ""} onChange={ingresarValoresMemoria} variant="outlined" fullWidth label="Ingrese Nombre y Apellidos"/>
                    </Grid>
                    <Grid item xs={12} md={6}>
                        <TextField name="username" value={usuario.username || ""} onChange={ingresarValoresMemoria} variant="outlined" fullWidth label="Ingrese Username"/>
                    </Grid>
                    <Grid item xs={12} md={6}>
                        <TextField name="email" value={usuario.email || ""} onChange={ingresarValoresMemoria} variant="outlined" fullWidth label="Ingrese email"/>
                    </Grid>
                    <Grid item xs={12} md={6}>
                        <TextField name="password" value={usuario.password || ""} onChange={ingresarValoresMemoria} type="password" variant="outlined" fullWidth label="Ingrese Password"/>
                    </Grid>
                    <Grid item xs={12} md={6}>
                        <TextField name="confirmarPassword" value={usuario.confirmarPassword || ""} onChange={ingresarValoresMemoria} type="password" variant="outlined" fullWidth label="Confirme Password"/>
                    </Grid>
                    <Grid item xs={12} md={12}>
                        <ImageUploader 
                            withIcon = {false}
                            key={fotoKey}
                            singleImage = {true}
                            buttonText = "Seleccione una imagen de perfil"
                            onChange = {subirFoto}
                            imgExtension = {[".jpg",".gif",".png",".jpeg"]}
                            maxFileSize = {5242880}
                        />
                    </Grid>
                </Grid>
                <Grid container justify="center">
                    <Grid item xs={12} md={6}>
                        <Button type="submit" onClick = {guardarUsuario} fullWidth variant="contained" size="large" color="primary" style={style.submit}>
                            Guardar Datos
                        </Button>
                    </Grid>
                </Grid>
            </form>
            </div>
        </Container>
    );
};

export default PerfilUsuario;