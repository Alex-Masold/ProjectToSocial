import type { User } from '@/models/User';
import axios from 'axios';

const API_URL: string = import.meta.env.VITE_API_URL;

export async function getUsers() {
  return await axios.get(`${API_URL}/api/users`);
}

export async function getUser(id: number) {
  return await axios.get(`${API_URL}/api/users/${id}`);
}

export async function createUser(user: User) {
  return await axios.post(`${API_URL}/api/users`, user);
}

export async function deleteUser(id: number) {
  return await axios.delete(`${API_URL}/api/users/${id}`);
}

export async function updateUserName(id: number, firstName: string, LastName: string, Family: string) {
  return await axios.patch(`${API_URL}/api/users/${id}`, {
    firstName: firstName,
    lastName: LastName,
    family: Family
  });   
}

export async function updateUserEmail(id: number, email: string) {
  return await axios.patch(`${API_URL}/api/users/${id}`, {
    email: email
  });
}

export async function updateUserPassword(id: number, password: string, passwordConfirm: string) {
  return await axios.patch(`${API_URL}/api/users/${id}`, {
    password: password,
    passwordConfirm: passwordConfirm
  });
}

export async function updateUser(id: number, user: object) {
  return await axios.patch(`${API_URL}/api/users/${id}`, user);
}